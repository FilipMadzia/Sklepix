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
					"Alejki - dodawanie", "Alejki - wyswietlanie", "Alejki - zmienianie", "Alejki - usuwanie",
					"Kategorie - dodawanie", "Kategorie - wyswietlanie", "Kategorie - zmienianie", "Kategorie - usuwanie",
					"Produkty - dodawanie", "Produkty - wyswietlanie", "Produkty - zmienianie", "Produkty - usuwanie",
					"Polki - dodawanie", "Polki - wyswietlanie", "Polki - zmienianie", "Polki - usuwanie"
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
