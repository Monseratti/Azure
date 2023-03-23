using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


var builder = new HostBuilder();
builder.ConfigureWebJobs(options =>
{
	options.AddAzureStorage();
	options.AddAzureStorageCoreServices();
});
builder.ConfigureLogging((httpContext, consoleBuilder) =>
{
	var key = httpContext.Configuration["APPINSIGHTS_INSTRUMENTATIONKEY"];
	consoleBuilder.AddConsole();
	consoleBuilder.AddApplicationInsightsWebJobs((ai) =>
	{
		ai.InstrumentationKey = key;
	});
});
var host = builder.Build();

using (host)
{
	host.Run();
}

