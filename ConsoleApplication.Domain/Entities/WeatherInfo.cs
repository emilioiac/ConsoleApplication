using System;

namespace ConsoleApplication.Domain.Entities
{
    public class WeatherInfo
    {
        public DateTime Time { get; set; }
        public double Temperature_2m { get; set; }
        public double Wind_Speed_10m { get; set; }
    }
}
