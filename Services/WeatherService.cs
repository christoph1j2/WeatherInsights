using LeuzeWeather.Models;
using System.Net.Http.Json;

namespace LeuzeWeather.Services
{
    /// <summary>
    /// Main class for communicating with the Open-Meteo Forecast API.
    /// </summary>
    public class WeatherService
    {
        private readonly HttpClient _api;

        /// <summary>
        /// Class constructor, DI
        /// </summary>
        /// <param name="api">The API HttpClient</param>
        public WeatherService(HttpClient api)
        {
            this._api = api;
        }

        /// <summary>
        /// Builds the URL and asynchronously fetches the data into the 
        /// ForecastResultWrapper class.
        /// </summary>
        /// <param name="lat">Latitude.</param>
        /// <param name="lon">Longitude.</param>
        /// <returns>Returns current weather and forecast for next 3 days.</returns>
        public async Task<ForecastResultWrapper?> GetWeatherAsync(double lat, double lon)
        {
            string url = $"v1/forecast?latitude={lat}&longitude={lon}&current=temperature_2m,relative_humidity_2m,wind_speed_10m&daily=temperature_2m_max,temperature_2m_min&forecast_days=3";
            var wrapper = await _api.GetFromJsonAsync<ForecastResultWrapper>(url);
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
