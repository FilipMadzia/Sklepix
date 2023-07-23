using Microsoft.AspNetCore.Identity;

namespace Sklepix.Data.Seeds
{
	public class RoleSeeder
	{
		public static async Task Seed(WebApplication app)
		{
			using(IServiceScope scope = app.Services.CreateScope())
			{
				RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
				string[] roles = new string[]
				{
					"Administrator",
					"Alejki.C", "Alejki.R", "Alejki.U", "Alejki.D",
					"Kategorie.C", "Kategorie.R", "Kategorie.U", "Kategorie.D",
					"Produkty.C", "Produkty.R", "Produkty.U", "Produkty.D",
					"Polki.C", "Polki.R", "Polki.U", "Polki.D"
				};

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
