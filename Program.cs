using Microsoft.EntityFrameworkCore;
using Sklepix.Data;
using Sklepix.Repositories;
using Microsoft.AspNetCore.Identity;
using Sklepix.Data.Entities;
using Sklepix.Data.Seeds;

namespace Sklepix
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddDbContext<SklepixContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("SklepixContext") ?? throw new InvalidOperationException("Connection string 'SklepixContext' not found.")));

			builder.Services.AddDefaultIdentity<UserEntity>(options =>
			{
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequiredLength = 4;
				options.Password.RequiredUniqueChars = 0;
				options.Password.RequireDigit = false;
				options.SignIn.RequireConfirmedAccount = false;
				options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ";
			})
				.AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<SklepixContext>();

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
			app.UseAuthentication(); ;

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");
			app.MapRazorPages();

			// seedowanie roli administrator i pracownik
			await RoleSeeder.Seed(app);
			// seedowanie konta administratora (admin@admin, admin)
			await UserSeeder.Seed(app);

			app.Run();
		}
	}
}
