using Microsoft.AspNetCore.Mvc;

namespace WeatherMoscow.Host.Controllers;

public class WeatherForecastController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Insert()
    {
        return View();
    }
}