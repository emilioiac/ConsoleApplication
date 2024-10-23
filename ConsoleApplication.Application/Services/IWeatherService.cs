using ConsoleApplication.Domain.Entities;
using System.Threading.Tasks;

namespace ConsoleApplication.Application.Services
{
    public interface IWeatherService
    {
        Task<WeatherInfo> GetWeatherInfoAsync();
    }
}
