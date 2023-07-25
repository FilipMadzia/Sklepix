using Microsoft.AspNetCore.Identity;
using Sklepix.Data.Entities;

namespace Sklepix.Data.Seeds
{
	public class UserSeeder
	{
		public static async Task Seed(WebApplication app)
		{
			using(IServiceScope scope = app.Services.CreateScope())
			{
				UserManager<UserEntity> userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();

				string email = "admin@admin";
				string password = "admin";

				if(await userManager.FindByEmailAsync(email) == null)
				{
					UserEntity user = new UserEntity()
					{
						UserName = "administrator",
						FirstName = "Jacek",
						LastName = "Kowalski",
						Email = email,
						CreationTime = DateTime.Now,
						EmailConfirmed = true,
					};

					await userManager.CreateAsync(user, password);

					await userManager.AddToRoleAsync(user, "Administrator");
				}
			}
		}
	}
}
