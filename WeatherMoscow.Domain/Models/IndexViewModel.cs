namespace WeatherMoscow.Domain.Entities;

/// <summary>
/// View model for pagination.
/// </summary>
public class IndexViewModel
{
    /// <summary>
    /// Data to list on one page.
    /// </summary>
    public IEnumerable<WeatherForecast> WeatherForecasts { get; set; }
    /// <summary>
    /// Page info.
    /// </summary>
    public PageViewModel PageViewModel { get; set; }
}