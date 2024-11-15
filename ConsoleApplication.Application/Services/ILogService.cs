using System.Runtime.CompilerServices;

namespace ConsoleApplication.Application.Services
{
    public interface ILogService
    {
        void MethodBegin([CallerMemberName] string callerMemberName = "");
    }
}
