namespace WeatherMoscow.Domain.Entities;

public class IndexViewModel
{
    public IEnumerable<WeatherForecast> WeatherForecasts { get; set; }
    public PageViewModel PageViewModel { get; set; }
}