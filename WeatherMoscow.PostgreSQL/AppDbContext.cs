using Microsoft.EntityFrameworkCore;
using WeatherMoscow.Domain.Entities;

namespace WeatherMoscow.PostgreSQL;

/// <summary>
/// Contains WeatherForecasts DbSet
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
    {
        Database.EnsureCreated();
    }

    public DbSet<WeatherForecast> WeatherForecasts;
}