using Microsoft.AspNetCore.Http;
using WeatherMoscow.Domain.Entities;

namespace WeatherMoscow.Domain.Abstractions;

/// <summary>
/// Repository for working with the 
/// </summary>
public interface IWeatherForecastRepository
{
    /// <summary>
    /// Gets all Weather forecasts
    /// </summary>
    /// <returns>IEnumerable<WeatherForecast></returns>
    public IndexViewModel GetPage(string monthFilter, string yearFilter, int page);

    /// <summary>
    /// Load Weather forecasts
    /// </summary>
    /// <param name="weatherForecasts">Forecast to load</param>
    /// <returns>true if loaded, false if not</returns>
    public bool Load(IFormFileCollection weatherForecasts);
}