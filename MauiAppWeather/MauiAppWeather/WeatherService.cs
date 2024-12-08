using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiAppWeather
{
    public class WeatherService
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private string apiKey = "9e82a8e542b3603dcfb99d9b0da2a2a0";

        public async Task<WeatherResponse> GetCurrentWeatherAsync(string cityName)
        {
            try 
            { 
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={apiKey}&units=metric";
            var response = await httpClient.GetStringAsync(url);
            System.Diagnostics.Debug.WriteLine("Response: " + response);
                 
                var weatherData = JsonSerializer.Deserialize<WeatherResponse>(response);
            return weatherData;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Ошибка при запросе: " + ex.Message);
                return null;
            }
    
        }

    }
}
