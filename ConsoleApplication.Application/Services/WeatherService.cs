using ConsoleApplication.Domain.Entities;
using ConsoleApplication.Domain.Interfaces;
using ConsoleApplication.Domain.Interfaces.Repositories;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace ConsoleApplication.Application.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly CustomConfiguration configuration;
        private readonly IWeatherInfoRepository repository;

        public WeatherService(
            CustomConfiguration configuration,
            IWeatherInfoRepository repository)
        {
            this.configuration = configuration;
            this.repository = repository;
        }

        public async Task<WeatherInfo> GetWeatherInfoAsync()
        {
            if (configuration == null || string.IsNullOrEmpty(configuration.WeatherInfoUrl))
                return null;

            if (!Uri.IsWellFormedUriString(configuration.WeatherInfoUrl, UriKind.Absolute))
                return null;

            var body = await repository.GetAsync(configuration.WeatherInfoUrl);

            var result = JObject.Parse(body);

            if (result == null)
                return null;

            var info = result["current"].ToObject<WeatherInfo>();

            return info;
        }
    }
}
