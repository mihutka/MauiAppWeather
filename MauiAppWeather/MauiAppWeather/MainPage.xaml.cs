
using Microsoft.Maui.Controls;
using System;

namespace MauiAppWeather
{
    public partial class MainPage : ContentPage
    {
        private readonly DatabaseService _dbService;
        private readonly WeatherService _weatherService;
        private readonly IServiceProvider _serviceProvider;

        public MainPage(DatabaseService dbService, WeatherService weatherService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _dbService = dbService;
            _weatherService = weatherService;
            _serviceProvider = serviceProvider;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateFavoriteCities();
        }

        private void UpdateFavoriteCities()
        {
            FavoriteCitiesView.ItemsSource = _dbService.GetFavoriteCities();
        }

        private async void OnShowWeatherClicked(object sender, EventArgs e)
        {
            var cityName = CityEntry.Text?.Trim();
            if (string.IsNullOrEmpty(cityName))
            {
                await DisplayAlert("Ошибка", "Введите название города", "OK");
                return;
            }

            var weather = await _weatherService.GetCurrentWeatherAsync(cityName);
            if (weather == null)
            {
                await DisplayAlert("Ошибка", "Не удалось получить данные о погоде.", "OK");
                return;
            }

            // Получаем WeatherPage из DI
            var weatherPage = _serviceProvider.GetService<WeatherPage>();
            if (weatherPage == null)
            {
                await DisplayAlert("Ошибка", "Не удалось создать страницу погоды.", "OK");
                return;
            }

            weatherPage.SetWeather(weather);
            await Navigation.PushAsync(weatherPage);
        }

        private void OnAddCityClicked(object sender, EventArgs e)
        {
            var cityName = FavCityEntry.Text?.Trim();
            if (!string.IsNullOrEmpty(cityName))
            {
                _dbService.AddFavoriteCity(cityName);
                UpdateFavoriteCities();
                FavCityEntry.Text = string.Empty;
            }
        }

        private void OnDeleteCityClicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.BindingContext is FavoriteCity city)
            {
                _dbService.RemoveFavoriteCity(city);
                UpdateFavoriteCities();
            }
        }

        private async void OnFavoriteCitySelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection == null || e.CurrentSelection.Count == 0)
                return;

            var selectedCity = e.CurrentSelection[0] as FavoriteCity;
            if (selectedCity == null)
                return;

            // Очищаем выбор
            FavoriteCitiesView.SelectedItem = null;

            var weather = await _weatherService.GetCurrentWeatherAsync(selectedCity.CityName);
            if (weather == null)
            {
                await DisplayAlert("Ошибка", $"Не удалось получить данные о погоде для города {selectedCity.CityName}.", "OK");
                return;
            }

            var weatherPage = _serviceProvider.GetService<WeatherPage>();
            if (weatherPage == null)
            {
                await DisplayAlert("Ошибка", "Не удалось создать страницу погоды.", "OK");
                return;
            }

            weatherPage.SetWeather(weather);
            await Navigation.PushAsync(weatherPage);
        }
    }
}
