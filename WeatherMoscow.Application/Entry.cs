using Microsoft.Extensions.DependencyInjection;
using WeatherMoscow.Domain.Abstractions;

namespace WeatherMoscow.Application;

public static class Entry
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IDataExtractor, ExcelDataExtractor>();

        return services;
    }
}