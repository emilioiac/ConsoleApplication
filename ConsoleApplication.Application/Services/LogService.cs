using log4net;
using log4net.Appender;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace ConsoleApplication.Application.Services
{
    public class LogService : ILogService
    {
        private readonly ILog logger;

        public LogService(ILog log)
        {
            this.logger = log;
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void DebugMethodStarted([CallerMemberName] string callerMemberName = "")
        {
            logger.Debug($"{callerMemberName} STARTED");
        }

        public void DebugMethodCompleted([CallerMemberName] string callerMemberName = "")
        {
            logger.Debug($"{callerMemberName} COMPLETED");
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Error(Exception exception)
        {
            logger.Error(exception);
        }

        public void Error(string message, Exception exception)
        {
            logger.Error(message, exception);
        }

        public void Fatal(string message)
        {
            logger.Fatal(message);
        }

        public void Fatal(Exception exception)
        {
            logger.Fatal(exception);
        }

        public void Fatal(string message, Exception exception)
        {
            logger.Fatal(message, exception);
        }

        public void Info(string message)
        {
            logger.Info(message);
        }

        public void Warn(string message)
        {
            logger.Warn(message);
        }

        #region Method

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void MethodBegin([CallerMemberName] string callerMemberName = "")
        {
            logger.Info($"BEGIN: method [{callerMemberName}]");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void MethodBegin(string message, [CallerMemberName] string callerMemberName = "")
        {
            logger.Info($"BEGIN: method [{callerMemberName}]. Message: {message}");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void MethodBegin(object parameter, [CallerMemberName] string callerMemberName = "")
        {
            var parameterString = ConvertParametersToString(parameter);
            logger.Info($"BEGIN: method [{callerMemberName}]. Parameters: {parameterString}");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void MethodBegin(object parameter1, object parameter2, [CallerMemberName] string callerMemberName = "")
        {
            var parameterString = ConvertParametersToString(parameter1, parameter2);
            logger.Info($"BEGIN: method [{callerMemberName}]. Parameters: {parameterString}");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void MethodBegin(object parameter1, object parameter2, object parameter3, [CallerMemberName] string callerMemberName = "")
        {
            var parameterString = ConvertParametersToString(parameter1, parameter2, parameter3);
            logger.Info($"BEGIN: method [{callerMemberName}]. Parameters: {parameterString}");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void MethodBegin(object parameter1, object parameter2, object parameter3, object parameter4, [CallerMemberName] string callerMemberName = "")
        {
            var parameterString = ConvertParametersToString(parameter1, parameter2, parameter3, parameter4);
            logger.Info($"BEGIN: method [{callerMemberName}]. Parameters: {parameterString}");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void MethodBegin(object parameter1, object parameter2, object parameter3, object parameter4, object parameter5, [CallerMemberName] string callerMemberName = "")
        {
            var parameterString = ConvertParametersToString(parameter1, parameter2, parameter3, parameter4, parameter5);
            logger.Info($"BEGIN: method [{callerMemberName}]. Parameters: {parameterString}");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void MethodCompleted([CallerMemberName] string callerMemberName = "")
        {
            logger.Info($"COMPLETED: method [{callerMemberName}]");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void MethodCompleted(string message, [CallerMemberName] string callerMemberName = "")
        {
            logger.Info($"COMPLETED: method [{callerMemberName}]. Message: {message}");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void MethodCompleted(object result, [CallerMemberName] string callerMemberName = "")
        {
            logger.Info($"COMPLETED: method [{callerMemberName}]. Result [{JsonConvert.SerializeObject(result)}]");
        }

        #endregion

        #region Scope

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ScopeBegin(AssemblyName callerIdentifier, [CallerMemberName] string callerMemberName = "")
        {
            logger.Info($"BEGIN: method [{callerMemberName}] called by [{callerIdentifier.Name}]");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ScopeBegin(string message, AssemblyName callerIdentifier, [CallerMemberName] string callerMemberName = "")
        {
            logger.Info($"BEGIN: method [{callerMemberName}] called by [{callerIdentifier.Name}]. Message: {message}");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ScopeBegin(object parameter, AssemblyName callerIdentifier, [CallerMemberName] string callerMemberName = "")
        {
            logger.Info($"BEGIN: method [{callerMemberName}] called by [{callerIdentifier.Name}]. Parameters: {ConvertParametersToString(parameter)}");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ScopeBegin(object parameter1, object parameter2, AssemblyName callerIdentifier, [CallerMemberName] string callerMemberName = "")
        {
            var parametersString = ConvertParametersToString(parameter1, parameter2);
            logger.Info($"BEGIN: method [{callerMemberName}] called by [{callerIdentifier.Name}]. Parameters: {parametersString}");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ScopeBegin(object parameter1, object parameter2, object parameter3, AssemblyName callerIdentifier, [CallerMemberName] string callerMemberName = "")
        {
            var parametersString = ConvertParametersToString(parameter1, parameter2, parameter3);
            logger.Info($"BEGIN: method [{callerMemberName}] called by [{callerIdentifier.Name}]. Parameters: {parametersString}");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ScopeBegin(object parameter1, object parameter2, object parameter3, object parameter4, AssemblyName callerIdentifier, [CallerMemberName] string callerMemberName = "")
        {
            var parametersString = ConvertParametersToString(parameter1, parameter2, parameter3, parameter4);
            logger.Info($"BEGIN: method [{callerMemberName}] called by [{callerIdentifier.Name}]. Parameters: {parametersString}");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ScopeBegin(object parameter1, object parameter2, object parameter3, object parameter4, object parameter5, AssemblyName callerIdentifier, [CallerMemberName] string callerMemberName = "")
        {
            var parametersString = ConvertParametersToString(parameter1, parameter2, parameter3, parameter4, parameter5);
            logger.Info($"BEGIN: method [{callerMemberName}] called by [{callerIdentifier.Name}]. Parameters: {parametersString}");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ScopeCompleted(AssemblyName callerIdentifier, [CallerMemberName] string callerMemberName = "")
        {
            logger.Info($"COMPLETED: method [{callerMemberName}] called by [{callerIdentifier.Name}]");
        }

        #endregion

        public string GetLogsPath()
        {
            return GetPath("file-logs");
        }

        private string GetPath(string appenderName)
        {
            var appenders = this.logger.Logger.Repository.GetAppenders();
            if (appenders.Any(_ => _.Name.Equals(appenderName)))
            {
                IAppender appender = this.logger.Logger.Repository.GetAppenders().Where(_ => _.Name == appenderName).FirstOrDefault();
                return ((FileAppender)appender).File;
            }
            return string.Empty;
        }

        private string ConvertParametersToString(params object[] parameters)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in parameters)
            {
                sb.AppendLine();
                var type = item.GetType();
                sb.Append($"\tType [{type}] | ");

                if (type == typeof(string))
                    sb.Append($"Value [{(string)item}]");
                else if (type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
                {
                    sb.Append($"Values:");
                    foreach (var subitem in (System.Collections.IEnumerable)item)
                    {
                        sb.AppendLine();
                        sb.Append("\t\t" + JsonConvert.SerializeObject(subitem));
                    }
                }
                else
                {
                    sb.Append($"Value [{JsonConvert.SerializeObject(item)}]");
                }
            }

            return sb.ToString();
        }
    }
}
