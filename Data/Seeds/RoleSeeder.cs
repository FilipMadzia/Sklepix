using Microsoft.AspNetCore.Identity;

namespace Sklepix.Data.Seeds
{
	public class RoleSeeder
	{
		public static async Task Seed(WebApplication app)
		{
			using(var scope = app.Services.CreateScope())
			{
				RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
				string[] roles = new string[] { "Administrator", "Pracownik" };

				foreach(string role in roles)
				{
					if(!await roleManager.RoleExistsAsync(role))
					{
						await roleManager.CreateAsync(new IdentityRole(role));
					}
				}
			}
		}
	}
}
