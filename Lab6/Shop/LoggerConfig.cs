using Serilog;
using Serilog.Extensions.Logging;
using Shop.Services.DbServices;

namespace Shop;

public static class LoggerConfig
{
	private static IServiceCollection _services;

	public static void ConfigLogger(this IServiceCollection services)
	{
		_services = services ?? throw new ArgumentNullException(nameof(services));
		ConfigLogForService<ICartService>("Logs/CartService.log");
		ConfigLogForService<ICategoryService>("Logs/CategoryService.log");
		ConfigLogForService<IClientService>("Logs/ClientService.log");
		ConfigLogForService<IDiscountService>("Logs/DiscountService.log");
		ConfigLogForService<IEmployeeService>("Logs/EmployeeService.log");
		ConfigLogForService<IOrderService>("Logs/OrderService.log");
		ConfigLogForService<IProductService>("Logs/ProductService.log");
		ConfigLogForService<IReviewService>("Logs/ReviewService.log");
		ConfigLogForService<IRoleService>("Logs/RoleService.log");
	}

	private static void ConfigLogForService<T>(string fileName)
	{
		var logger = new LoggerConfiguration().WriteTo.File(fileName).CreateLogger();
		var serilogLoggerFactory = new SerilogLoggerFactory(logger);
		var serviceLogger = serilogLoggerFactory.CreateLogger<T>();
		_services.AddSingleton(serviceLogger);
	}
}
