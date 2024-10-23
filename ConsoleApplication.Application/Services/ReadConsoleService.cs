using System;

namespace ConsoleApplication.Application.Services
{
    public class ReadConsoleService : IReadConsoleService
    {
        private readonly ILogConsoleService logConsoleService;
        private readonly IInputValidatorService inputValidatorService;

        public ReadConsoleService(
            ILogConsoleService logConsoleService,
            IInputValidatorService inputValidatorService
            )
        {
            this.logConsoleService = logConsoleService;
            this.inputValidatorService = inputValidatorService;
        }

        private string GetConsoleValue() => Console.ReadLine();

        public int GetIntConsoleValue()
        {
            var isInputValid = false;
            string value = null;

            while (!isInputValid)
            {
                var numberOfNumbers = GetConsoleValue();
                isInputValid = inputValidatorService.IsIntInputValid(numberOfNumbers);

                if (isInputValid)
                    value = numberOfNumbers;
                else
                    logConsoleService.LogStringInConsole("Invalid input, insert again");
            }

            return int.Parse(value);
        }

        public bool GetExitConsoleValue()
        {
            var isInputValid = false;
            string value = null;

            while (!isInputValid)
            {
                var exitValue = GetConsoleValue();
                isInputValid = inputValidatorService.IsExitInputValid(exitValue.ToLower());

                if (isInputValid)
                    value = exitValue.ToLower();
                else
                    logConsoleService.LogStringInConsole("Invalid input, insert again");
            }

            return value == "y" ? true : false;
        }
    }
}
