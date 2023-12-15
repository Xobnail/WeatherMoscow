using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WeatherMoscow.PostgreSQL;

/// <summary>
/// Entry class for PostgreSQL database
/// </summary>
public static class Entry
{
    /// <summary>
    /// Registers DbContext
    /// </summary>
    /// <param name="services">Services to add to</param>
    /// <param name="connectionString">Connection string for db</param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        return services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
    }
}