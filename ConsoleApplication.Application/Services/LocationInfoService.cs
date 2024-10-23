using ConsoleApplication.Domain.Entities;
using ConsoleApplication.Domain.Interfaces.Repositories;
using Newtonsoft.Json.Linq;
using System;
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

        private async Task<LocationInfo> GetAsync()
        {
            if (configuration == null || string.IsNullOrEmpty(configuration.LocationInfoUrl))
                return null;

            if (!Uri.IsWellFormedUriString(configuration.LocationInfoUrl, UriKind.Absolute))
                return null;

            var body = await locationInfoRepository.GetAsync(configuration.LocationInfoUrl);

            var result = JObject.Parse(body);

            if (result == null)
                return null;

            var locationInfo = result["address"].ToObject<LocationInfo>();

            return locationInfo;
        }

        public async Task<string> GetAsStringAsync()
        {
            var location = await GetAsync();
            if (location == null)
                return null;

            return location.ToString();
        }
    }
}
