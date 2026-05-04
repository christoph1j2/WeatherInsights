using LeuzeWeather.Models;
using System.Net.Http.Json;

namespace LeuzeWeather.Services
{
    /// <summary>
    /// Main class for communicating with the Open-Meteo API.
    /// </summary>
    public class GeocodingService
    {
        private readonly HttpClient _api;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="api">The API HttpClient</param>
        public GeocodingService(HttpClient api)
        {
            this._api = api;
        }

        public async Task<GeocodingResult?> GetCityAsync(string name)
        {
            string url = $"v1/search?name={name}&count=5";
            var wrapper = await _api.GetFromJsonAsync<GeocodingResultWrapper>(url);

        }
    }
}
