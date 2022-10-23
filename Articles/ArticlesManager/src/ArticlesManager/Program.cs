using Serilog;
using ArticlesManager.Extensions.Application;
using ArticlesManager.Extensions.Host;
using ArticlesManager.Extensions.Services;

var builder = WebApplication.CreateBuilder(args);

// Adding configuration from App settings
var configBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

if (builder.Environment.IsDevelopment())
    configBuilder.AddJsonFile("appsettings.Development.json");
else
    configBuilder.AddJsonFile("appsettings.Production.json");

var config = configBuilder.Build();

builder.Configuration.AddConfiguration(config);
//END ADDED
builder.Host.AddLoggingConfiguration(builder.Environment);

builder.ConfigureServices();
var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// For elevated security, it is recommended to remove this middleware and set your server to only listen on https.
// A slightly less secure option would be to redirect http to 400, 505, etc.
app.UseHttpsRedirection();

app.UseCors("ArticlesManagerCorsPolicy");


// TODO to keep comment below?
app.UseSerilogRequestLogging();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/api/health");
    endpoints.MapControllers();
});

app.UseSwaggerExtension(builder);

try
{
    Log.Information("Starting application");
    await app.RunAsync();
}
catch (Exception e)
{
    Log.Error(e, "The application failed to start correctly");
    throw;
}
finally
{
    Log.Information("Shutting down application");
    Log.CloseAndFlush();
}

// Make the implicit Program class public so the functional test project can access it
public partial class Program { }