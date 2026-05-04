using System.Text.Json.Serialization;

namespace LeuzeWeather.Models
{
    /// <summary>
    /// Model class for the Meteo API daily result from
    /// Open-Meteo.
    /// </summary>
    public class ForecastResultDaily
    {
        [JsonPropertyName("time")]
        public string[]? DateArr { get; set; }

        [JsonPropertyName("temperature_2m_max")]
        public double[]? TempMaxArr { get; set; }

        [JsonPropertyName("temperature_2m_min")]
        public double[]? TempMinArr { get; set; }
    }

    /// <summary>
    /// Model class for the Meteo API current result from
    /// Open-Meteo
    /// </summary>
    public class ForecastResultCurrent
    {
        [JsonPropertyName("time")]
        public string? Date { get; set; }

        [JsonPropertyName("temperature_2m")]
        public double Temp { get; set; }

        [JsonPropertyName("relative_humidity_2m")]
        public double Humidity { get; set; }

        [JsonPropertyName("wind_speed_10m")]
        public double WindSpeed { get; set; }
    }

    /// <summary>
    /// Holds forecast for next 3 days and today's forecast
    /// from the results it fetches from the API.
    /// </summary>
    public class ForecastResultWrapper
    {
        [JsonPropertyName("daily")]
        public ForecastResultDaily? ForecastDaily { get; set; }

        [JsonPropertyName("current")]
        public ForecastResultCurrent? ForecastCurrent { get; set; }
    }
}
