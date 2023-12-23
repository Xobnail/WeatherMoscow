using Microsoft.AspNetCore.Http;
using WeatherMoscow.Domain.Entities;

namespace WeatherMoscow.Domain.Abstractions;

/// <summary>
/// Repository for working with the weather forecast entity.
/// </summary>
public interface IWeatherForecastRepository
{
    /// <summary>
    /// Filters list of weather forecasts and gets one page of weather forecasts.
    /// </summary>
    /// <param name="monthFilter">Month filter.</param>
    /// <param name="yearFilter">Year filter.</param>
    /// <param name="page">Page number.</param>
    /// <returns>Weather forecasts pagination view model.</returns>
    public Task<IndexViewModel> GetPageAsync(string monthFilter, string yearFilter, int page);

    /// <summary>
    /// Loads Weather forecasts.
    /// </summary>
    /// <param name="weatherForecasts">Forecast to load.</param>
    public Task LoadAsync(IFormFileCollection weatherForecasts);
}