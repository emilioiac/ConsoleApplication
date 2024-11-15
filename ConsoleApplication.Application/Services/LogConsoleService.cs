using System;

namespace ConsoleApplication.Application.Services
{
    public class LogConsoleService : ILogConsoleService
    {
        private readonly ILogService logService;

        public LogConsoleService(ILogService logService)
        {
            this.logService = logService;
        }

        public void LogIntInConsole(int value)
        {
            logService.MethodBegin();

            Console.WriteLine(value);

            logService.MethodCompleted();
        }

        public void LogStringInConsole(string message)
        {
            logService.MethodBegin();

            Console.WriteLine(message);

            logService.MethodCompleted();
        }
    }
}
