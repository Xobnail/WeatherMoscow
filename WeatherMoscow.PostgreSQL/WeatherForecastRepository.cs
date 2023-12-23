using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WeatherMoscow.Domain.Abstractions;
using WeatherMoscow.Domain.Entities;

namespace WeatherMoscow.PostgreSQL;

/// <summary>
/// Implementation of IWeatherForecastRepository.
/// Contains load and get page methods.
/// </summary>
public class WeatherForecastRepository : IWeatherForecastRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IDataExtractor _dataExtractor;
 
    public WeatherForecastRepository(AppDbContext dbContext, IDataExtractor dataExtractor)
    {
        _dbContext = dbContext;
        _dataExtractor = dataExtractor;
    }

    /// <summary>
    /// Filters list of weather forecasts and gets one page of weather forecasts.
    /// </summary>
    /// <param name="monthFilter">Month filter.</param>
    /// <param name="yearFilter">Year filter.</param>
    /// <param name="page">Page number.</param>
    /// <returns>Weather forecasts pagination view model.</returns>
    public async Task<IndexViewModel> GetPageAsync(string monthFilter, string yearFilter, int page)
    {
        const int pageSize = 10;

        var sourceForecasts = _dbContext.WeatherForecasts.AsQueryable();

        Filter(ref sourceForecasts, monthFilter, yearFilter);

        var count = await sourceForecasts.CountAsync();
        var items = await sourceForecasts.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
 
        var pageViewModel = new PageViewModel(count, page, pageSize);
        var viewModel = new IndexViewModel
        {
            PageViewModel = pageViewModel,
            WeatherForecasts = items
        };
        
        return viewModel;
    }

    /// <summary>
    /// Filters weather forecasts by month and year.
    /// </summary>
    /// <param name="source">Weather forecasts to filter.</param>
    /// <param name="monthFilter">Month filter.</param>
    /// <param name="yearFilter">Year filter.</param>
    private static void Filter(ref IQueryable<WeatherForecast> source, string monthFilter, string yearFilter)
    {
        if (!source.Any())
        {
            return;
        }
        
        if (!string.IsNullOrEmpty(monthFilter))
        {
            source = source.Where(wf => wf.Date.Month.ToString().Contains(monthFilter));
        }
        
        if (!string.IsNullOrEmpty(yearFilter))
        {
            source = source.Where(wf => wf.Date.Year.ToString().Contains(yearFilter));
        }
    }

    /// <summary>
    /// Loads Weather forecasts.
    /// </summary>
    /// <param name="weatherForecasts">Forecast to load.</param>
    public async Task LoadAsync(IFormFileCollection files)
    {
        ArgumentNullException.ThrowIfNull(files);

        var weatherForecasts = _dataExtractor.Extract(files);

        await _dbContext.WeatherForecasts.AddRangeAsync(weatherForecasts);
        await _dbContext.SaveChangesAsync();
    }
}