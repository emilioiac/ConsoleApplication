using System.Reflection;
using System;
using System.Runtime.CompilerServices;

namespace ConsoleApplication.Application.Services
{
    public interface ILogService
    {
        void Debug(string message);
        void DebugMethodStarted([CallerMemberName] string callerMemberName = "");
        void DebugMethodCompleted([CallerMemberName] string callerMemberName = "");
        void Info(string message);
        void Warn(string message);
        void Error(string message);
        void Error(Exception exception);
        void Error(string message, Exception exception);
        void Fatal(string message);
        void Fatal(Exception exception);
        void Fatal(string message, Exception exception);

        void MethodBegin([CallerMemberName] string callerMemberName = "");
        void MethodBegin(string message, [CallerMemberName] string callerMemberName = "");
        void MethodBegin(object parameter, [CallerMemberName] string callerMemberName = "");
        void MethodBegin(object parameter1, object parameter2, [CallerMemberName] string callerMemberName = "");
        void MethodBegin(object parameter1, object parameter2, object parameter3, [CallerMemberName] string callerMemberName = "");
        void MethodBegin(object parameter1, object parameter2, object parameter3, object parameter4, [CallerMemberName] string callerMemberName = "");
        void MethodBegin(object parameter1, object parameter2, object parameter3, object parameter4, object parameter5, [CallerMemberName] string callerMemberName = "");

        void MethodCompleted([CallerMemberName] string callerMemberName = "");
        void MethodCompleted(string message, [CallerMemberName] string callerMemberName = "");
        void MethodCompleted(object result, [CallerMemberName] string callerMemberName = "");

        void ScopeBegin(AssemblyName callerIdentifier, [CallerMemberName] string callerMemberName = "");
        void ScopeBegin(string message, AssemblyName callerIdentifier, [CallerMemberName] string callerMemberName = "");
        void ScopeBegin(object parameter, AssemblyName callerIdentifier, [CallerMemberName] string callerMemberName = "");
        void ScopeBegin(object parameter1, object parameter2, AssemblyName callerIdentifier, [CallerMemberName] string callerMemberName = "");
        void ScopeBegin(object parameter1, object parameter2, object parameter3, AssemblyName callerIdentifier, [CallerMemberName] string callerMemberName = "");
        void ScopeBegin(object parameter1, object parameter2, object parameter3, object parameter4, AssemblyName callerIdentifier, [CallerMemberName] string callerMemberName = "");
        void ScopeBegin(object parameter1, object parameter2, object parameter3, object parameter4, object parameter5, AssemblyName callerIdentifier, [CallerMemberName] string callerMemberName = "");

        void ScopeCompleted(AssemblyName callerIdentifier, [CallerMemberName] string callerMemberName = "");

        string GetLogsPath();
    }
}
