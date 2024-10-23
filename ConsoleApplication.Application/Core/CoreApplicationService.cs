using ConsoleApplication.Application.Services;
using System.Threading.Tasks;

namespace ConsoleApplication.Application.Core
{
    public class CoreApplicationService : ICoreApplicationService
    {
        private readonly ILogConsoleService logConsoleService;
        private readonly IReadConsoleService readConsoleService;
        private readonly IWeatherService weatherService;
        private readonly ILocationInfoService locationInfoService;

        public CoreApplicationService(
            ILogConsoleService logConsoleService,
            IReadConsoleService readConsoleService,
            IWeatherService weatherService,
            ILocationInfoService locationInfoService
            )
        {
            this.logConsoleService = logConsoleService;
            this.readConsoleService = readConsoleService;
            this.weatherService = weatherService;
            this.locationInfoService = locationInfoService;
        }

        public async Task RunAsync()
        {
            var exit = false;

            while (!exit)
            {
                logConsoleService.LogStringInConsole("Start Program");

                var weatherInfo = await weatherService.GetWeatherInfoAsync();

                if(weatherInfo != null)
                {
                    var location = await locationInfoService.GetAsStringAsync();

                    logConsoleService.LogStringInConsole($"Weather For {location}");
                    logConsoleService.LogStringInConsole($"Time: {weatherInfo?.Time}");
                    logConsoleService.LogStringInConsole($"Temperature: {weatherInfo?.Temperature_2m}");
                    logConsoleService.LogStringInConsole($"Wind: {weatherInfo?.Wind_Speed_10m}");
                } 
                else
                {
                    logConsoleService.LogStringInConsole("Cannot retrieve data");
                }

                logConsoleService.LogStringInConsole("You want to exit? Y/N");
                exit = readConsoleService.GetExitConsoleValue();

                if (exit)
                    logConsoleService.LogStringInConsole("End Program");
            }
        }
    }
}