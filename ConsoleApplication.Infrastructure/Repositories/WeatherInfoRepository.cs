﻿using ConsoleApplication.Domain.Interfaces;
using ConsoleApplication.Domain.Interfaces.Repositories;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApplication.Infrastructure.Repositories
{
    public class WeatherInfoRepository : IWeatherInfoRepository
    {
        private readonly HttpClient client;

        public WeatherInfoRepository(
            HttpClient client)
        {
            this.client = client;
        }

        public async Task<string> GetAsync(string url)
        {

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url)
            };

            using (var response = await client?.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}