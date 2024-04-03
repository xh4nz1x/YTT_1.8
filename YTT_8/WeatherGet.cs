namespace Weather;

public class WeatherGet
{
    public class WeatherResult
    {
        public Main Main { get; set; }
        public Wind Wind { get; set; }
        public WeatherInfo[] Weather { get; set; }
    }
    
    public class Main
    {
        public double Temp { get; set; }
        public double Feels_Like { get; set; }
        public int Humidity { get; set; }
        public int Pressure { get; set; }
    }

    public class Wind
    {
        public double Speed { get; set; }
    }

    public class WeatherInfo
    {
        public string Description { get; set; }
    }

}