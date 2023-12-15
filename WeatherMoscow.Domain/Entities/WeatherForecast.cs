using System.ComponentModel.DataAnnotations;

namespace WeatherMoscow.Domain.Entities;

public class WeatherForecast
{
    [Required]
    public int Id { get; set; }
    [Required]
    public DateOnly Date { get; set; }
    [Required]
    public TimeOnly Time { get; set; }
    [Required]
    public int Temperature { get; set; }
    [Required]
    [Range(1, 100)]
    public int RelativeHumidity { get; set; }
    [Required]
    public int DewPoint { get; set; }
    [Required]
    public int AtmospherePressure { get; set; }
    [Required]
    public string WindDirection { get; set; }
    [Required]
    public int WindSpeed { get; set; }
    [Range(1, 100)]
    public int Cloudiness { get; set; }
    [Required]
    public int CloudCeiling { get; set; }
    
    public int HorizontalVisibility { get; set; }
    
    public string WeatherEvents { get; set; }
}