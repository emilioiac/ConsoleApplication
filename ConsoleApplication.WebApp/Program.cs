using ConsoleApplication.Application;
using ConsoleApplication.WebApp.Components;
using log4net.Repository;
using log4net;
using System.Reflection;
using Blazorise;
using Blazorise.Icons.FontAwesome;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddBlazorBootstrap();

builder.Services.Scan(scan =>
{
    scan.FromApplicationDependencies(x => x.FullName.StartsWith("ConsoleApplication"))
        .AddClasses(publicOnly: true)
        .AsMatchingInterface((service, filter) =>
            filter.Where(interfaceType =>
                interfaceType.Name.Equals($"I{service.Name}", StringComparison.OrdinalIgnoreCase)
                ))
    .WithTransientLifetime();
});

var customConfig = GetCustomConfigurationOrDefault(builder.Configuration);
builder.Services.AddSingleton(customConfig);

var httpClient = GetHttpClient();
builder.Services.AddSingleton(httpClient);

var logger = RegisterLogger(builder.Configuration);
builder.Services.AddSingleton(logger);

builder.Services
    .AddBlazorise(_ =>
    {
        _.IconSize = IconSize.Large;
    })
    .AddBlazorBootstrap()
    .AddFontAwesomeIcons();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

CustomConfiguration GetCustomConfigurationOrDefault(IConfiguration configuration)
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
            if (latitude != null)
                section.Value = section.Value.Replace("#/Latitude", latitude.Value);

            var longitude = sections.FirstOrDefault(_ => _.Path == nameof(CustomConfiguration.Longitude));
            if (longitude != null)
                section.Value = section.Value.Replace("#/Longitude", longitude.Value);
        }

        property.SetValue(customConfiguration, section.Value);
    }

    return customConfiguration;
}

HttpClient GetHttpClient()
{
    var httpClient = new HttpClient();
    httpClient.DefaultRequestHeaders.Add("User-Agent", "ConsoleApplication/1.0 (myEmail@house.com)");

    return httpClient;
}

ILog RegisterLogger(IConfiguration configuration)
{

    var assemblyPath = Assembly.GetExecutingAssembly().Location;
    var directoryName = Path.GetDirectoryName(assemblyPath);

    var assemblyName = Assembly.GetCallingAssembly().GetName().Name;
    var logConfigFileName = $"{assemblyName}.log4net.config";

    var filePath = Path.Combine(directoryName, logConfigFileName);
    var fileInfo = new FileInfo(filePath);

    var repoName = $"{assemblyName}.ILoggerRepository";

    ILoggerRepository repo;
    try
    {
        repo = LogManager.GetRepository(repoName);
    }
    catch (log4net.Core.LogException)
    {
        repo = LogManager.CreateRepository(repoName);
    }

    if (fileInfo.Exists && !repo.Configured)
    {
        var sections = configuration.GetChildren();
        if (!sections.Any())
            return null;

        var folderPath = sections.FirstOrDefault(_ => _.Path == "LogFolder");
        if (folderPath == null)
            return null;

        GlobalContext.Properties["LogFolder"] = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), folderPath.Value);
        log4net.Config.XmlConfigurator.Configure(repo, fileInfo);
    }

    return LogManager.GetLogger(repoName, $"{assemblyName}.ILog");
}
