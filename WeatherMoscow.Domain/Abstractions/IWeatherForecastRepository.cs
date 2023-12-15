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
    public IEnumerable<WeatherForecast> GetAll();

    /// <summary>
    /// Load Weather forecasts
    /// </summary>
    /// <param name="weatherForecasts">Forecast to load</param>
    /// <returns>true if loaded, false if not</returns>
    public bool Load(IEnumerable<WeatherForecast> weatherForecasts);
}