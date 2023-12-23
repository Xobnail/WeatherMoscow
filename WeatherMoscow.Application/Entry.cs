using Microsoft.Extensions.DependencyInjection;
using WeatherMoscow.Domain.Abstractions;

namespace WeatherMoscow.Application;

/// <summary>
/// Entry class for Application layer.
/// </summary>
public static class Entry
{
    /// <summary>
    /// Registers services.
    /// </summary>
    /// <param name="services">Services to add to.</param>
    /// <returns>Service collection.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IDataExtractor, ExcelDataExtractor>();

        return services;
    }
}