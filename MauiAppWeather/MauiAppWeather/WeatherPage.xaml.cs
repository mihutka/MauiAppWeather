namespace MauiAppWeather 
{ 
public partial class WeatherPage : ContentPage
{
    private WeatherResponse _weather;

    public WeatherPage()
    {
        InitializeComponent();
    }

    public void SetWeather(WeatherResponse weather)
    {
        _weather = weather;
        ShowWeather();
    }

    private void ShowWeather()
    {
        if (_weather == null)
        {
            CityLabel.Text = "��� ������ � ������";
            TemperatureLabel.Text = "";
            DescriptionLabel.Text = "";
            WeatherIconImage.Source = null;
            return;
        }

        CityLabel.Text = !string.IsNullOrEmpty(_weather.Name)
            ? $"�����: {_weather.Name}"
            : "�������� ������ ����������";

        TemperatureLabel.Text = _weather.Main != null
            ? $"�����������: {_weather.Main.Temp} �C"
            : "����������� ����������";

        if (_weather.Weather != null && _weather.Weather.Length > 0 && _weather.Weather[0] != null)
        {
            DescriptionLabel.Text = $"��������: {_weather.Weather[0].Description}";
            var iconCode = _weather.Weather[0].Icon;
            var iconUrl = $"https://openweathermap.org/img/wn/{iconCode}@2x.png";
            WeatherIconImage.Source = ImageSource.FromUri(new Uri(iconUrl));
        }
        else
        {
            DescriptionLabel.Text = "�������� ������ ����������";
            WeatherIconImage.Source = null;
        }
    }
}
}