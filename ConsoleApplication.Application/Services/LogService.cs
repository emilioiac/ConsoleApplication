using log4net;
using System.Runtime.CompilerServices;

namespace ConsoleApplication.Application.Services
{
    public class LogService : ILogService
    {
        private readonly ILog log;

        public LogService(ILog log)
        {
            this.log = log;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void MethodBegin([CallerMemberName] string callerMemberName = "")
        {
            log.Info($"BEGIN: method [{callerMemberName}]");
        }
    }
}
