using Microsoft.EntityFrameworkCore;
using Sklepix.Data;
using Sklepix.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SklepixContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("SklepixContext") ?? throw new InvalidOperationException("Connection string 'SklepixContext' not found.")));

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<AisleRepository>();
builder.Services.AddTransient<CategoryRepository>();
builder.Services.AddTransient<ProductRepository>();
builder.Services.AddTransient<ShelfRepository>();

var app = builder.Build();

if(!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
