using Microsoft.AspNetCore.Http;
using WeatherMoscow.Domain.Entities;

namespace WeatherMoscow.Domain.Abstractions;

/// <summary>
/// Interface for working with data extraction.
/// </summary>
public interface IDataExtractor
{
    /// <summary>
    /// Extract data from multiple files.
    /// </summary>
    /// <param name="files">Files to extract data from.</param>
    /// <returns>A list of weather forecasts.</returns>
    public List<WeatherForecast> Extract(IFormFileCollection files);
}