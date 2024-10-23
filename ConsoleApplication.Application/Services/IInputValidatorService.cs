namespace ConsoleApplication.Application.Services
{
    public interface IInputValidatorService
    {
        void ValidateInputOrThrow(string input);
        bool IsIntInputValid(string numberOfNumbers);
        bool IsExitInputValid(string exitValue);
    }
}
