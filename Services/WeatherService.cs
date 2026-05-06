using LeuzeWeather.Models;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;

namespace LeuzeWeather.Services
{
    /// <summary>
    /// Main class for communicating with the Open-Meteo Forecast API.
    /// </summary>
    public class WeatherService
    {
        private readonly HttpClient _api;
        private readonly Dictionary<string, ForecastResultWrapper> _cache; // reduce API calls by caching results for already searched cities, key is "name,country,region"

        /// <summary>
        /// Class constructor, DI
        /// </summary>
        /// <param name="api">The API HttpClient</param>
        public WeatherService(HttpClient api)
        {
            this._api = api;
            this._cache = new Dictionary<string, ForecastResultWrapper>();
        }

        /// <summary>
        /// Builds the URL and asynchronously fetches the data into the 
        /// ForecastResultWrapper class and saves it into cache.
        /// Otherwise loads the data from the cache.
        /// </summary>
        /// <param name="lat">Latitude.</param>
        /// <param name="lon">Longitude.</param>
        /// <returns>Returns current weather and forecast for next 3 days.</returns>
        public async Task<ForecastResultWrapper?> GetWeatherAsync(double lat, double lon, string? name, string? country, string? region)
        {
            ForecastResultWrapper? wrapper;
            string? key = $"{name},{country},{region}";

            if (_cache.ContainsKey(key)) 
            {
                wrapper = _cache.GetValueOrDefault(key);
            } 
            else 
            { 
                string url = $"v1/forecast?latitude={lat.ToString(System.Globalization.CultureInfo.InvariantCulture)}&longitude={lon.ToString(System.Globalization.CultureInfo.InvariantCulture)}&current=temperature_2m,relative_humidity_2m,wind_speed_10m,weather_code&daily=temperature_2m_max,temperature_2m_min,weather_code&forecast_days=4";
                wrapper = await _api.GetFromJsonAsync<ForecastResultWrapper>(url);
                if (wrapper != null) _cache.Add(key, wrapper);
            }
            if (wrapper != null
                &&
                wrapper.ForecastCurrent != null
                &&
                wrapper.ForecastDaily != null)
            {
                return wrapper;
            }
            else return null;
        }
    }
}
