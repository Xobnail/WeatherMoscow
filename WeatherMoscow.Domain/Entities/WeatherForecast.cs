namespace WeatherMoscow.Domain.Entities;

public class WeatherForecast
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly Time { get; set; }

    public double Temperature { get; set; }

    public int RelativeHumidity { get; set; }

    public double DewPoint { get; set; }

    public int AtmospherePressure { get; set; }

    public string? WindDirection { get; set; }

    public int? WindSpeed { get; set; }

    public int? Cloudiness { get; set; }

    public int? CloudCeiling { get; set; }

    public int? HorizontalVisibility { get; set; }

    public string? WeatherEvents { get; set; }
}