using System.Text.Json.Serialization;

namespace LeuzeWeather.Models
{
    /// <summary>
    /// Model class for the Geocoding API result from 
    /// Open-Meteo.
    /// </summary>
    public class GeocodingResult
    {
        public string? Name { get; set; }

        public double Latitude { get; set; }
        
        public double Longitude { get; set; }
        
        public string? Country { get; set; }
        
        [JsonPropertyName("admin1")] // the JSON returns admin1, we name it Region
        public string? Region { get; set; }
    }

    /// <summary>
    /// Holds a list of all the results it fetches from the API.
    /// </summary>
    public class GeocodingResultWrapper
    {
        public List<GeocodingResult>? Results { get; set; }
    }
}
