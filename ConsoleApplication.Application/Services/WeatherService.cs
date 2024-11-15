using ConsoleApplication.Domain.Entities;
using ConsoleApplication.Domain.Interfaces;
using ConsoleApplication.Domain.Interfaces.Repositories;
using log4net.Repository.Hierarchy;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace ConsoleApplication.Application.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly CustomConfiguration configuration;
        private readonly IWeatherInfoRepository repository;
        private readonly ILogService logService;

        public WeatherService(
            CustomConfiguration configuration,
            IWeatherInfoRepository repository,
            ILogService logService)
        {
            this.configuration = configuration;
            this.repository = repository;
            this.logService = logService;
        }

        public async Task<WeatherInfo> GetWeatherInfoAsync()
        {
            logService.MethodBegin();
            if (configuration == null || string.IsNullOrEmpty(configuration.WeatherInfoUrl))
            {
                logService.MethodCompleted();
                return null;
            }

            if (!Uri.IsWellFormedUriString(configuration.WeatherInfoUrl, UriKind.Absolute))
            {
                logService.MethodCompleted();
                return null;
            }

            var body = await repository.GetAsync(configuration.WeatherInfoUrl);

            var result = JObject.Parse(body);

            if (result == null)
            {
                logService.MethodCompleted();
                return null;
            }

            var info = result["current"].ToObject<WeatherInfo>();

            logService.MethodCompleted();
            return info;
        }
    }
}
