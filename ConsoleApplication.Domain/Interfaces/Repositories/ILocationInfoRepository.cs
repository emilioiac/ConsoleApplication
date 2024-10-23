using System.Threading.Tasks;

namespace ConsoleApplication.Domain.Interfaces.Repositories
{
    public interface ILocationInfoRepository
    {
        Task<string> GetAsync(string url);
    }
}
