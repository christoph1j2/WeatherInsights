
using LeuzeWeather.Models;

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
        public COMFORT_LEVEL ComputeForCurrent(ForecastResultCurrent forecastResultCurrent)
        {
            double Temperature = forecastResultCurrent.Temp;
            double Humidity = forecastResultCurrent.Humidity;
            double WindSpeed = forecastResultCurrent.WindSpeed;
            int WeatherCode = forecastResultCurrent.WeatherCode;
            int score = 0;

            if (15 <= Temperature && Temperature <= 28)
            {
                if (18 <= Temperature && Temperature <= 25) score += 2;
                else score += 1;
            }

            if (Humidity < 60) score += 2;
            else if (Humidity < 80) score += 1;

            if (WindSpeed < 20) score += 2;
            else if (WindSpeed < 40) score += 1;

            if (WeatherCode < 3) score += 2;
            else if (WeatherCode < 50) score += 1;

            if (score >= 6) return COMFORT_LEVEL.Good;
            else if (score >= 3) return COMFORT_LEVEL.Moderate;
            else return COMFORT_LEVEL.Bad;
        }
        public static int GetScore(ForecastResultCurrent forecast)
        {
            double Temperature = forecast.Temp;
            double Humidity = forecast.Humidity;
            double WindSpeed = forecast.WindSpeed;
            int WeatherCode = forecast.WeatherCode;
            int score = 0;

            if (15 <= Temperature && Temperature <= 28)
            {
                if (18 <= Temperature && Temperature <= 25) score += 2;
                else score += 1;
            }

            if (Humidity < 60) score += 2;
            else if (Humidity < 80) score += 1;

            if (WindSpeed < 20) score += 2;
            else if (WindSpeed < 40) score += 1;

            if (WeatherCode < 3) score += 2;
            else if (WeatherCode < 50) score += 1;

            return score;
        } 
    }

    public class ComfortServiceForecast
    {
        public COMFORT_LEVEL ComputeForForecast(ForecastResultDaily forecastResultDaily, int dayIndex)
        {
            Console.WriteLine($"dayIndex={dayIndex}, arrLen={forecastResultDaily.TempMaxArr?.Length}");
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
