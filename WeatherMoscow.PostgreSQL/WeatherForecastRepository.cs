﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NPOI.HSSF.Model;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using WeatherMoscow.Domain.Abstractions;
using WeatherMoscow.Domain.Entities;

namespace WeatherMoscow.PostgreSQL;

/// <summary>
/// Implementation of IWeatherForecastRepository
/// Contains read and write methods for WeatherForecasts
/// </summary>
public class WeatherForecastRepository : IWeatherForecastRepository
{
    private readonly IWebHostEnvironment _environment;
    private readonly AppDbContext _dbContext;
 
    public WeatherForecastRepository(IWebHostEnvironment environment, AppDbContext dbContext)
    {
        _environment = environment;
        _dbContext = dbContext;
    }

    /// <summary>
    /// Gets all Weather forecasts
    /// </summary>
    /// <returns>IEnumerable<WeatherForecast></returns>
    public IndexViewModel GetPage(string monthFilter, string yearFilter, int page)
    {
        const int pageSize = 10;

        var source = _dbContext.WeatherForecasts.AsQueryable();

        if (!string.IsNullOrEmpty(monthFilter))
        {
            source = source.Where(wf => wf.Date.Month.ToString().Contains(monthFilter));
        }
        
        if (!string.IsNullOrEmpty(yearFilter))
        {
            source = source.Where(wf => wf.Date.Year.ToString().Contains(yearFilter));
        }
        
        var count = source.Count();
        var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();
 
        var pageViewModel = new PageViewModel(count, page, pageSize);
        var viewModel = new IndexViewModel
        {
            PageViewModel = pageViewModel,
            WeatherForecasts = items
        };
        
        return viewModel;
    }

    /// <summary>
    /// Loads Weather forecasts
    /// </summary>
    /// <param name="weatherForecasts">Forecast to load</param>
    /// <returns>True if loaded, false if not</returns>
    public bool Load(IFormFileCollection files)
    {
        ArgumentNullException.ThrowIfNull(files);

        foreach (var file in files)
        {
            var workbook = new XSSFWorkbook(file.OpenReadStream());
            var weatherForecasts = new List<WeatherForecast>();
            
            foreach (var sheet in workbook)
            {
                for (var rowIndex = 4; rowIndex < sheet.LastRowNum; rowIndex++)
                {
                    var cells = sheet.GetRow(rowIndex).Cells;

                    while (cells.Count < 12)
                    {
                        cells.Add(null);
                    }
                    
                    weatherForecasts.Add(new WeatherForecast
                    {
                        Date = DateOnly.Parse(cells[0].ToString()!),
                        Time = TimeOnly.Parse(cells[1].ToString()!),
                        Temperature = cells[2].NumericCellValue,
                        RelativeHumidity = (int)cells[3].NumericCellValue,
                        DewPoint = cells[4].NumericCellValue,
                        AtmospherePressure = (int)cells[5].NumericCellValue,
                        WindDirection = !string.IsNullOrWhiteSpace(cells[6]?.StringCellValue)
                            ? cells[6].StringCellValue
                            : null,
                        WindSpeed = cells[7]?.CellType == CellType.Numeric
                            ? (int)cells[7].NumericCellValue
                            : null,
                        Cloudiness = cells[8]?.CellType == CellType.Numeric
                            ? (int)cells[8].NumericCellValue
                            : null,
                        CloudCeiling = cells[9]?.CellType == CellType.Numeric
                            ? (int)cells[9].NumericCellValue
                            : null,
                        HorizontalVisibility = cells[10]?.CellType == CellType.Numeric
                            ? (int)cells[10].NumericCellValue
                            : null,
                        WeatherEvents = !string.IsNullOrWhiteSpace(cells[11]?.StringCellValue)
                            ? cells[11].StringCellValue
                            : null
                    });
                }
            }
            
            _dbContext.WeatherForecasts.AddRange(weatherForecasts);
            _dbContext.SaveChanges();
        }

        return false;
    }
}