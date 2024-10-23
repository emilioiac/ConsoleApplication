using Autofac;
using ConsoleApplication.Application;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;

namespace ConsoleApplication.Presentation.Utils
{
    public class IoCConfigurator
    {
        public IContainer Container { get; private set; }

        public IoCConfigurator(IConfigurationRoot configuration)
        {
            ConfigureContainer(configuration);
        }


        private void ConfigureContainer(IConfiguration configuration, string loggerName = "")
        {

            var builder = new ContainerBuilder();

            var assemblies = GetAssemblies();

            builder.RegisterAssemblyTypes(assemblies)
                .Where(_ => !_.IsAbstract && _.IsPublic)
                .Where(_ => !IsSingleton(_))
                .Where(_ => _.Name.EndsWith("Repository") || _.Name.EndsWith("Service"))
                .Where(_ => !_.Name.StartsWith("Mock"))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assemblies)
                .Where(_ => !_.IsAbstract && _.IsPublic)
                .Where(_ => IsSingleton(_))
                .Where(_ => _.Name.EndsWith("Repository") || _.Name.EndsWith("Service"))
                .Where(_ => !_.Name.StartsWith("Mock"))
                .SingleInstance();

            var httpClient = GetHttpClient();

            builder.RegisterInstance(httpClient)
                .SingleInstance();

            var customConfig = GetCustomConfigurationOrDefault(configuration);

            builder.RegisterInstance(customConfig)
                .SingleInstance();

            builder.RegisterAssemblyModules(assemblies);

            Container = builder.Build();
        }

        private HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "ConsoleApplication/1.0 (myEmail@house.com)");

            return httpClient;
        }

        private CustomConfiguration GetCustomConfigurationOrDefault(IConfiguration configuration)
        {
            var sections = configuration.GetChildren();
            if (!sections.Any())
                return null;

            var properties = typeof(CustomConfiguration).GetProperties();

            if (!properties.Any(_ => sections.Select(_ => _.Path).Contains(_.Name)))
                return null;

            var customConfiguration = new CustomConfiguration();
            foreach (var section in sections)
            {
                var property = properties.FirstOrDefault(_ => _.Name == section.Path);
                if (property == null)
                    continue;

                if (section.Value.Contains("#/Latitude") && section.Value.Contains("#/Longitude"))
                {
                    var latitude = sections.FirstOrDefault(_ => _.Path == nameof(CustomConfiguration.Latitude));
                    if(latitude != null)
                        section.Value = section.Value.Replace("#/Latitude", latitude.Value);

                    var longitude = sections.FirstOrDefault(_ => _.Path == nameof(CustomConfiguration.Longitude));
                    if(longitude != null)
                        section.Value = section.Value.Replace("#/Longitude", longitude.Value);
                }

                property.SetValue(customConfiguration, section.Value);
            }

            return customConfiguration;
        }

        private Assembly[] GetAssemblies()
        {
            var partialAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            var infrastructureAssemblies = GetInfrastructureAssemblies();

            var result = new List<Assembly>();

            result.AddRange(partialAssemblies);
            result.Add(infrastructureAssemblies);

            return result.ToArray();
        }

        private Assembly GetInfrastructureAssemblies()
        {
            var assemblyPath = @"ConsoleApplication.Infrastructure.dll";
            return Assembly.LoadFrom(assemblyPath);

        }

        private bool IsSingleton(Type classType)
        {
            var attribute = Attribute.GetCustomAttribute(classType, typeof(SingletonAttribute));
            return attribute != null;
        }

        [AttributeUsage(AttributeTargets.Class)]
        private class SingletonAttribute : Attribute
        {
        }
    }
}
