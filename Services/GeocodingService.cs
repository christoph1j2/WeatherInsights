using LeuzeWeather.Models;
using System.Net.Http.Json;

namespace LeuzeWeather.Services
{
    /// <summary>
    /// Main class for communicating with the Open-Meteo Geocoding API.
    /// </summary>
    public class GeocodingService
    {
        private readonly HttpClient _api;
        private readonly Dictionary<string, GeocodingResultWrapper> _cache; // reduce API calls by caching results for already searched cities

        /// <summary>
        /// Class constructor, DI
        /// </summary>
        /// <param name="api">The API HttpClient</param>
        public GeocodingService(HttpClient api)
        {
            this._api = api;
            this._cache = new Dictionary<string, GeocodingResultWrapper>();
        }

        /// <summary>
        /// Builds the URL and asynchronously fetches the data into the Geocoding
        /// wrapper class.
        /// </summary>
        /// <param name="name">Name of the city</param>
        /// <returns>Returns the first result if not empty nor null.</returns>
        public async Task<List<GeocodingResult>?> GetCityAsync(string name)
        {
            GeocodingResultWrapper? wrapper;
            string key = name.ToLower(); // case-insensitive caching
            if (_cache.ContainsKey(key))
            {
                wrapper = _cache.GetValueOrDefault(key);
            } 
            else 
            {
                try
                {
                    string url = $"v1/search?name={key}&count=5";
                    wrapper = await _api.GetFromJsonAsync<GeocodingResultWrapper>(url);
                    if (wrapper != null) _cache.Add(key, wrapper);
                }
                catch (Exception ex) 
                {
                    Console.WriteLine($"Error fetching city data: {ex.Message}");
                    return null;
                }
            }

            if (wrapper != null && wrapper.Results != null && wrapper.Results.Count > 0)
            {
                return wrapper.Results;
            }
            else return null;
        }
    }
}
