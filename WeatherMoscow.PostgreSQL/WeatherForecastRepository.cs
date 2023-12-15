using WeatherMoscow.Domain.Abstractions;
using WeatherMoscow.Domain.Entities;

namespace WeatherMoscow.PostgreSQL;

/// <summary>
/// Implementation of IWeatherForecastRepository
/// Contains read and write methods for WeatherForecasts
/// </summary>
public class WeatherForecastRepository : IWeatherForecastRepository
{
    /// <summary>
    /// Gets all Weather forecasts
    /// </summary>
    /// <returns>IEnumerable<WeatherForecast></returns>
    public IEnumerable<WeatherForecast> GetAll()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Loads Weather forecasts
    /// </summary>
    /// <param name="weatherForecasts">Forecast to load</param>
    /// <returns>True if loaded, false if not</returns>
    public bool Load(IEnumerable<WeatherForecast> weatherForecasts)
    {
        throw new NotImplementedException();
    }
}