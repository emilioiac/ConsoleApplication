using ConsoleApplication.Application;
using ConsoleApplication.WebApp.Components;


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
