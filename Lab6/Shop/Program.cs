using Microsoft.EntityFrameworkCore;
using Shop;
using Shop.Models;
using Shop.Services.Authentication;
using Shop.Services.DbServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DbLabsContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DbLabsContext")));

builder.Services.AddScoped<IShopAuthenticationService, ShopAuthenticationService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();

builder.Services.AddAuthentication().AddCookie("Cookies");
builder.Services.ConfigLogger();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
