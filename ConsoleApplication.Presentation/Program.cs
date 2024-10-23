using Autofac;
using ConsoleApplication.Application.Core;
using ConsoleApplication.Presentation.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace ConsoleApplication.Presentation
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();



            var iocConfigurator = new IoCConfigurator(configuration);
            var coreApplication = iocConfigurator.Container.Resolve<ICoreApplicationService>();

            await coreApplication.RunAsync();
        }
    }
}
