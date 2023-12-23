using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WeatherMoscow.Domain.Abstractions;

namespace WeatherMoscow.PostgreSQL;

/// <summary>
/// Entry class for PostgreSQL database.
/// </summary>
public static class Entry
{
    /// <summary>
    /// Registers DbContext and weather forecast repository.
    /// </summary>
    /// <param name="services">Services to add to.</param>
    /// <param name="connectionString">Connection string for db.</param>
    /// <returns>Service collection.</returns>
    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
        return services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
    }
}