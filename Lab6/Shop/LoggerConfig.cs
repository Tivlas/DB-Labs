using System.Runtime.CompilerServices;
using Serilog.Extensions.Logging;
using Serilog;
using Shop.Services.DbServices;

namespace Shop;

public static class LoggerConfig
{
	public static void ConfigLogger(this IServiceCollection services)
	{
		ConfigClientService(services);
		ConfigEmployeeService(services);
	}

	private static void ConfigClientService(IServiceCollection services)
	{
		var logger = new LoggerConfiguration().WriteTo.File("Logs/ClientService.log").CreateLogger();
		var serilogLoggerFactory = new SerilogLoggerFactory(logger);
		var serviceLogger = serilogLoggerFactory.CreateLogger<IClientService>();
		services.AddSingleton(serviceLogger);
	}

	private static void ConfigEmployeeService(IServiceCollection services)
	{
		var logger = new LoggerConfiguration().WriteTo.File("Logs/EmployeeService.log").CreateLogger();
		var serilogLoggerFactory = new SerilogLoggerFactory(logger);
		var serviceLogger = serilogLoggerFactory.CreateLogger<IEmployeeService>();
		services.AddSingleton(serviceLogger);
	}
}
