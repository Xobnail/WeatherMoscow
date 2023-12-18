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
    
    public IActionResult Index()
    {
        var weatherForecasts = _weatherForecastRepository.GetAll();
        
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