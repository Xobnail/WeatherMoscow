using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace WeatherMoscow.Host.Controllers;

public class WeatherForecastController : Controller
{
    private readonly IWebHostEnvironment _environment;
 
    public WeatherForecastController(IWebHostEnvironment environment)
    {
        _environment = environment;
    }
    
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Insert()
    {
        return View();
    }

    [HttpPost]
    public IActionResult UploadFiles(IFormFileCollection files)
    {
        var wwwrootPath = _environment.WebRootPath;
 
        var path = Path.Combine(wwwrootPath, "Uploads");
        
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
 
        foreach (var file in files)
        {
            var fileName = Path.GetFileName(file.FileName);
            using (var stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                file.CopyTo(stream);
                ViewBag.Message += $"<b>{fileName}</b> uploaded.<br />";
            }
        }
 
        return View("Insert");
    }
}