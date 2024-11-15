using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;

namespace ConsoleApplication.Application.Services
{
    public class InputValidatorService : IInputValidatorService
    {
        private readonly ILogService logService;

        public InputValidatorService(ILogService logService)
        {
            this.logService = logService;
        }

        public bool IsExitInputValid(string exitValue)
        {
            logService.MethodBegin();

            if (exitValue == null)
            {
                logService.MethodCompleted();
                return false;
            }

            var validExitValues = new List<string> { "y", "n" };

            if (!validExitValues.Contains(exitValue))
            {
                logService.MethodCompleted();
                return false;
            }

            logService.MethodCompleted();
            return true;
        }

        public bool IsIntInputValid(string input)
        {
            logService.MethodBegin();

            if (input == null)
            {
                logService.MethodCompleted();
                return false;
            }

            int value;
            var isParsed = int.TryParse(input, out value);

            if (!isParsed)
            {
                logService.MethodCompleted();
                return false;
            }

            logService.MethodCompleted();
            return true;
        }

        public void ValidateInputOrThrow(string input)
        {
            logService.MethodBegin();

            if (input == null)
            {
                var errorMessage = "Input string is null";

                logService.Error(errorMessage);
                throw new Exception(errorMessage);
            }

            int value;
            var isParsed = int.TryParse(input, out value);

            if (!isParsed)
            {
                var errorMessage = "Provided input is not a int";

                logService.Error(errorMessage);
                throw new Exception(errorMessage);
            }

            logService.MethodCompleted();
        }
    }
}
