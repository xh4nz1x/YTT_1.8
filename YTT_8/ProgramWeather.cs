using Newtonsoft.Json;

namespace Weather
{
    class ProgramWeather
    {
        public static string path = "defaultCity.json";
        
        public static string apiKey = "ba1013fc0ed0c72824df076498f306c0";

        static async Task Main(string[] args)
        {
            Console.WriteLine("\n-----> Программа для просмотра погоды для нужного города <-----");
            bool work = true;
            bool continueWork = false;
            
            while (work == true)
            {
                if (File.Exists(path) == false)
                {
                    var file = File.Create(path);
                    file.Close();
                }
                
                string cityTitle = File.ReadAllText(path);
                string apiUrl;
                string city;
                if (string.IsNullOrEmpty(cityTitle) || continueWork == true)
                {
                    Console.Write("\n-----> Введите название города: ");
                    city = Console.ReadLine();
                    
                    Console.WriteLine($"\n-----> Сделать городом по умолчанию?\nВыберите: \n 1 - Да \n 2 - Нет \n");
                    string selectCommand1 = Console.ReadLine();

                    if (selectCommand1 == "1")
                    {
                        File.WriteAllText(path, city);
                    }

                    else if (selectCommand1 != "2")
                    {
                        Console.WriteLine("\n-----Данной команды - нет!-----");
                    }

                    apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric&lang=ru";
                }

                else
                {
                    city = Convert.ToString(cityTitle);
                    apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric&lang=ru";
                }

                using HttpClient client = new HttpClient();

                string json = await client.GetStringAsync(apiUrl);

                WeatherGet.WeatherResult weather = JsonConvert.DeserializeObject<WeatherGet.WeatherResult>(json);
                
                Console.WriteLine($"\n· Город: {city}");
                Console.WriteLine($"· Температура: {weather.Main.Temp}°C | Ощущается, как: {weather.Main.Feels_Like}°C");
                Console.WriteLine($"· Влажность воздуха: {weather.Main.Humidity} %");
                Console.WriteLine($"· Скорость ветра: {weather.Wind.Speed} м/с");
                Console.WriteLine($"· Атмосферное давление: {weather.Main.Pressure} мм рт. ст.");
                Console.WriteLine($"· Описание: {weather.Weather[0].Description}");

                Console.Write("\n---------------------------------------------");
                Console.WriteLine($"\nВыберите: \n 1 - Завершить работу программы \n 2 - Посмотреть прогноз, для другого города");
                Console.WriteLine("---------------------------------------------");
                string selectcommand2 = Console.ReadLine();
                
                if (selectcommand2 == "1")
                {
                    continueWork = false;
                    work = false;
                }
                else if (selectcommand2 == "2")
                {
                    continueWork = true;
                }
            }
        }
    }
}
