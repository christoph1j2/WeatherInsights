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

        /// <summary>
        /// Class constructor, DI
        /// </summary>
        /// <param name="api">The API HttpClient</param>
        public GeocodingService(HttpClient api)
        {
            this._api = api;
        }

        /// <summary>
        /// Builds the URL and asynchronously fetches the data into the Geocoding
        /// wrapper class.
        /// </summary>
        /// <param name="name">Name of the city</param>
        /// <returns>Returns the first result if not empty nor null.</returns>
        public async Task<List<GeocodingResult>?> GetCityAsync(string name)
        {
            string url = $"v1/search?name={name}&count=5";
            var wrapper = await _api.GetFromJsonAsync<GeocodingResultWrapper>(url);
            if (wrapper != null && wrapper.Results != null && wrapper.Results.Count > 0)
            {
                return wrapper.Results;
            }
            else return null;
        }
    }
}
