using System;

namespace ConsoleApplication.Application.Services
{
    public class LogConsoleService : ILogConsoleService
    {
        public void LogIntInConsole(int value)
        {
            Console.WriteLine(value);
        }

        public void LogStringInConsole(string message)
        {
            Console.WriteLine(message);
        }
    }
}
