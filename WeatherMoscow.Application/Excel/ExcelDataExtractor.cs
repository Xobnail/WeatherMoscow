using Microsoft.AspNetCore.Http;
using NPOI.HSSF.Model;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using WeatherMoscow.Domain.Abstractions;
using WeatherMoscow.Domain.Entities;

namespace WeatherMoscow.Application;

public class ExcelDataExtractor : IDataExtractor
{
    public List<WeatherForecast> Extract(IFormFileCollection files)
    {
        var weatherForecasts = new List<WeatherForecast>();
        
        foreach (var file in files)
        {
            ExtractFromFileToList(file, weatherForecasts);
        }

        return weatherForecasts;
    }
    
    private static void ExtractFromFileToList(IFormFile file, List<WeatherForecast> weatherForecasts)
    {
        var workbook = new XSSFWorkbook(file.OpenReadStream());
        
        foreach (var sheet in workbook)
        {
            for (var rowIndex = 4; rowIndex < sheet.LastRowNum; rowIndex++)
            {
                var cells = sheet.GetRow(rowIndex).Cells;

                while (cells.Count < 12)
                {
                    cells.Add(null);
                }

                MapWeatherForecast(weatherForecasts, cells);
            }
        }
    }

    private static void MapWeatherForecast(List<WeatherForecast> weatherForecasts, List<ICell> cells)
    {
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