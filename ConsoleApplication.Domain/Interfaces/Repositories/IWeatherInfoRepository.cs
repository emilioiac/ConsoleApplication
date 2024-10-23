using ConsoleApplication.Domain.Entities;
using System.Threading.Tasks;

namespace ConsoleApplication.Domain.Interfaces.Repositories
{
    public interface IWeatherInfoRepository
    {
        Task<string> GetAsync(string url);
    }
}
