using Microsoft.AspNetCore.Mvc;

namespace WeatherMoscow.Host.Controllers;

/// <summary>
/// Home page controller.
/// </summary>
public class HomeController : Controller
{
    /// <summary>
    /// Gets home page.
    /// </summary>
    /// <returns>Home page.</returns>
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}