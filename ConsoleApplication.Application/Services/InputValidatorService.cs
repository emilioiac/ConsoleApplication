using System;
using System.Collections.Generic;

namespace ConsoleApplication.Application.Services
{
    public class InputValidatorService : IInputValidatorService
    {
        public bool IsExitInputValid(string exitValue)
        {
            if (exitValue == null)
                return false;

            var validExitValues = new List<string> { "y", "n" };

            if (!validExitValues.Contains(exitValue))
                return false;

            return true;
        }

        public bool IsIntInputValid(string input)
        {
            if (input == null)
                return false;

            int value;
            var isParsed = int.TryParse(input, out value);

            if (!isParsed)
                return false;

            return true;
        }

        public void ValidateInputOrThrow(string input)
        {
            if (input == null)
                throw new Exception("Input string is null");

            int value;
            var isParsed = int.TryParse(input, out value);

            if (!isParsed)
                throw new Exception("Provided input is not a int");

        }
    }
}
