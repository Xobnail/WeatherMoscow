using Microsoft.AspNetCore.Http;
using WeatherMoscow.Domain.Entities;

namespace WeatherMoscow.Domain.Abstractions;

public interface IDataExtractor
{
    public List<WeatherForecast> Extract(IFormFileCollection files);
}