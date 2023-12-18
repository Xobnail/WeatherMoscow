using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using WeatherMoscow.Domain.Abstractions;

namespace WeatherMoscow.Host.Controllers;

public class WeatherForecastController : Controller
{
    private readonly IWebHostEnvironment _environment;
    private readonly IWeatherForecastRepository _weatherForecastRepository;
 
    public WeatherForecastController(
        IWebHostEnvironment environment, 
        IWeatherForecastRepository weatherForecastRepository)
    {
        _environment = environment;
        _weatherForecastRepository = weatherForecastRepository;
    }
    
    public IActionResult Index(string? monthFilter, string currentMonthFilter, string? yearFilter, string currentYearFilter, int page = 1)
    {
        if (monthFilter != null || yearFilter != null)
        {
            page = 1;
        }

        monthFilter ??= currentMonthFilter;
        yearFilter ??= currentYearFilter;
        
        ViewData["currentMonthFilter"] = monthFilter;
        ViewData["currentYearFilter"] = yearFilter;

        var weatherForecasts = _weatherForecastRepository.GetPage(monthFilter, yearFilter, page);
        
        return View(weatherForecasts);
    }

    public IActionResult Insert()
    {
        return View();
    }

    [HttpPost]
    public IActionResult UploadFiles(IFormFileCollection files)
    {
        _weatherForecastRepository.Load(files);

        return View("Insert");
    }
}