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

        public LocationInfoService(
            CustomConfiguration configuration,
            ILocationInfoRepository locationInfoRepository)
        {
            this.configuration = configuration;
            this.locationInfoRepository = locationInfoRepository;
        }

        private async Task<LocationInfo> GetOrDefaultAsync()
        {
            if (configuration == null || string.IsNullOrEmpty(configuration.LocationInfoUrl))
                return null;

            if (!Uri.IsWellFormedUriString(configuration.LocationInfoUrl, UriKind.Absolute))
                return null;

            var body = await locationInfoRepository.GetAsync(configuration.LocationInfoUrl);

            if (string.IsNullOrEmpty(body))
                return null;

            var isValidJson = IsValidJson(body);

            if (!isValidJson)
                return null;

            var result = JObject.Parse(body);

            var locationInfo = GetLocationInfoOrDefault(result);

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
            var location = await GetOrDefaultAsync();
            if (location == null)
                return null;

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
