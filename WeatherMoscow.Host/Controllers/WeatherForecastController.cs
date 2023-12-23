using Microsoft.AspNetCore.Mvc;
using WeatherMoscow.Domain.Abstractions;

namespace WeatherMoscow.Host.Controllers;

/// <summary>
/// A controller for work with weather forecasts.
/// </summary>
public class WeatherForecastController : Controller
{
    private readonly IWeatherForecastRepository _weatherForecastRepository;
 
    public WeatherForecastController(IWeatherForecastRepository weatherForecastRepository)
    {
        _weatherForecastRepository = weatherForecastRepository;
    }
    
    /// <summary>
    /// Gets one page of weather forecasts.
    /// </summary>
    /// <param name="monthFilter">Month filter.</param>
    /// <param name="currentMonthFilter">Current month filter.</param>
    /// <param name="yearFilter">Year filter.</param>
    /// <param name="currentYearFilter">Current year filter.</param>
    /// <param name="page">Current page.</param>
    /// <returns>A view for one page.</returns>
    [HttpGet]
    public async Task<IActionResult> Index(string? monthFilter, string currentMonthFilter, string? yearFilter, string currentYearFilter, int page = 1)
    {
        if (monthFilter != null || yearFilter != null)
        {
            page = 1;
        }

        monthFilter ??= currentMonthFilter;
        yearFilter ??= currentYearFilter;
        
        ViewData["currentMonthFilter"] = monthFilter;
        ViewData["currentYearFilter"] = yearFilter;

        var weatherForecasts = await _weatherForecastRepository.GetPageAsync(monthFilter, yearFilter, page);

        if (!weatherForecasts.WeatherForecasts.Any())
        {
            ViewData["errorMessage"] = "Нет данных.";

            return View(weatherForecasts);
        }
        
        return View(weatherForecasts);
    }

    /// <summary>
    /// Gets page to load weather forecasts data.
    /// </summary>
    /// <returns>A view to load data.</returns>
    [HttpGet]
    public IActionResult Insert()
    {
        return View();
    }

    /// <summary>
    /// Uploads excel files, which contains weather forecasts data.
    /// </summary>
    /// <param name="files">Files to load.</param>
    /// <returns>The Insert view with status.</returns>
    [HttpPost]
    public async Task<IActionResult> UploadFiles(IFormFileCollection files)
    {
        try
        {
            await _weatherForecastRepository.LoadAsync(files);
            ViewData["uploadStatus"] = "Файлы загружены.";
        }
        catch (ArgumentNullException)
        {
            ViewData["uploadStatus"] = "Пустые файлы!";
        }
        catch (Exception)
        {
            ViewData["uploadStatus"] = "Файл не подлежит загрузке.";
        }

        return View("Insert");
    }
}