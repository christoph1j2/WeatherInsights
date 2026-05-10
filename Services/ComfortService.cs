
using LeuzeWeather.Models;
using MudBlazor;

namespace LeuzeWeather.Services
{
    public enum COMFORT_LEVEL
    {
        Good,
        Moderate,
        Bad
    }

    public class ComfortServiceCurrent
    {
        private const int MIN_COMFORTABLE_TEMP = 15;
        private const int MAX_COMFORTABLE_TEMP = 28;
        private const int MIN_IDEAL_TEMP = 18;
        private const int MAX_IDEAL_TEMP = 25;

        private const int MAX_COMFORTABLE_HUMIDITY = 80;
        private const int MAX_IDEAL_HUMIDITY = 60;

        private const int MAX_COMFORTABLE_WIND_SPEED = 40;
        private const int MAX_IDEAL_WIND_SPEED = 20;

        private const int MAX_COMFORTABLE_WEATHER_CODE = 50;
        private const int MAX_IDEAL_WEATHER_CODE = 3;


        /// <summary>
        /// Computes the comfort level from the COMFORT_LEVEL enum from current forecast.
        /// </summary>
        /// <param name="forecastResultCurrent"></param>
        /// <returns>Respective Comfort Level</returns>
        /// 
        public COMFORT_LEVEL ComputeForCurrent(ForecastResultCurrent forecastResultCurrent)
        {
            int score = GetScore(forecastResultCurrent);

            if (score >= 6) return COMFORT_LEVEL.Good;
            else if (score >= 3) return COMFORT_LEVEL.Moderate;
            else return COMFORT_LEVEL.Bad;
        }

        /// <summary>
        /// Gets the comfort score for the current forecast.
        /// </summary>
        /// <param name="forecast"></param>
        /// <returns>Integer representing the comfort level.</returns>
        public static int GetScore(ForecastResultCurrent forecast)
        {
            double Temperature = forecast.Temp;
            double Humidity = forecast.Humidity;
            double WindSpeed = forecast.WindSpeed;
            int WeatherCode = forecast.WeatherCode;
            int score = 0;

            if (MIN_COMFORTABLE_TEMP <= Temperature && Temperature <= MAX_COMFORTABLE_TEMP)
            {
                if (MIN_IDEAL_TEMP <= Temperature && Temperature <= MAX_IDEAL_TEMP) score += 2;
                else score += 1;
            }

            if (Humidity < MAX_IDEAL_HUMIDITY) score += 2;
            else if (Humidity < MAX_COMFORTABLE_HUMIDITY) score += 1;

            if (WindSpeed < MAX_IDEAL_WIND_SPEED) score += 2;
            else if (WindSpeed < MAX_COMFORTABLE_WIND_SPEED) score += 1;

            if (WeatherCode < MAX_IDEAL_WEATHER_CODE) score += 2;
            else if (WeatherCode < MAX_COMFORTABLE_WEATHER_CODE) score += 1;

            return score;
        }

        /// <summary>
        /// Takes a comfort level and returns the corresponding color for display purposes.
        /// </summary>
        /// <param name="level">The level used for the mapping</param>
        /// <returns>A color based on the comfort level</returns>
        public Color GetComfortColor(COMFORT_LEVEL level) => level switch
        {
            COMFORT_LEVEL.Good => Color.Success,
            COMFORT_LEVEL.Moderate => Color.Warning,
            COMFORT_LEVEL.Bad => Color.Error,
            _ => Color.Default
        };
    }

    public class ComfortServiceForecast
    {
        /// <summary>
        /// Computes the comfort level from the COMFORT_LEVEL enum from a date specified by dayIndex in the forecastResults.
        /// </summary>
        /// <param name="forecastResultDaily">ForecastResults</param>
        /// <param name="dayIndex">Index for a specific day from the results.</param>
        /// <returns>Comfort level</returns>
        public COMFORT_LEVEL ComputeForForecast(ForecastResultDaily forecastResultDaily, int dayIndex)
        {
            double MaxTemperature = forecastResultDaily.TempMaxArr?[dayIndex] ?? 1;
            double MinTemperature = forecastResultDaily.TempMinArr?[dayIndex] ?? 1;
            double AvgTemperature = (MaxTemperature + MinTemperature) / 2;
            int WeatherCode = forecastResultDaily.WeatherCode?[dayIndex] ?? 0;
            int score = 0;
            if (15 <= AvgTemperature && AvgTemperature <= 28)
            {
                if (18 <= AvgTemperature && AvgTemperature <= 25) score += 2;
                else score += 1;
            }

            if (WeatherCode < 3) score += 2;
            else if (WeatherCode < 50) score += 1;

            if (score >= 6) return COMFORT_LEVEL.Good;
            else if (score >= 3) return COMFORT_LEVEL.Moderate;
            else return COMFORT_LEVEL.Bad;
        }
    }
    }
