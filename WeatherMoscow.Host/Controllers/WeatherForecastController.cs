using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using WeatherMoscow.Domain.Abstractions;

namespace WeatherMoscow.Host.Controllers;

public class WeatherForecastController : Controller
{
    private readonly IWeatherForecastRepository _weatherForecastRepository;
 
    public WeatherForecastController(IWeatherForecastRepository weatherForecastRepository)
    {
        _weatherForecastRepository = weatherForecastRepository;
    }
    
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

    public IActionResult Insert()
    {
        return View();
    }

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