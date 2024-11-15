using ConsoleApplication.Domain.Entities;
using ConsoleApplication.Domain.Interfaces.Repositories;
using Newtonsoft.Json.Linq;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApplication.Application.Services
{
    public class LocationInfoService : ILocationInfoService
    {
        private readonly CustomConfiguration configuration;
        private readonly ILocationInfoRepository locationInfoRepository;
        private readonly ILogService logger;

        public LocationInfoService(
            CustomConfiguration configuration,
            ILocationInfoRepository locationInfoRepository,
            ILogService logger)
        {
            this.configuration = configuration;
            this.locationInfoRepository = locationInfoRepository;
            this.logger = logger;
        }

        private async Task<LocationInfo> GetOrDefaultAsync()
        {
            logger.MethodBegin();

            if (configuration == null || string.IsNullOrEmpty(configuration.LocationInfoUrl))
            {
                logger.MethodCompleted();
                return null;
            }

            if (!Uri.IsWellFormedUriString(configuration.LocationInfoUrl, UriKind.Absolute))
            {
                logger.MethodCompleted();
                return null;
            }

            var body = await locationInfoRepository.GetAsync(configuration.LocationInfoUrl);

            if (string.IsNullOrEmpty(body))
            {
                logger.MethodCompleted();
                return null;
            }

            var isValidJson = IsValidJson(body);

            if (!isValidJson)
            {
                logger.MethodCompleted();
                return null;
            }

            var result = JObject.Parse(body);

            var locationInfo = GetLocationInfoOrDefault(result);

            logger.MethodCompleted();
            return locationInfo;
        }

        private LocationInfo GetLocationInfoOrDefault(JObject result)
        {
            try
            {
                return result["address"].ToObject<LocationInfo>();
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public async Task<string> GetAsStringAsync()
        {
            logger.MethodBegin();

            var location = await GetOrDefaultAsync();
            if (location == null)
            {
                logger.MethodCompleted();
                return null;
            }

            logger.MethodCompleted();
            return location.ToString();
        }

        public bool IsValidJson(string jsonString)
        {
            try
            {
                using (JsonDocument doc = JsonDocument.Parse(jsonString))
                {
                    return true;
                }
            }
            catch (JsonException)
            {
                return false;
            }
        }
    }
}
