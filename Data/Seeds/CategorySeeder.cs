using Microsoft.EntityFrameworkCore;
using Sklepix.Data.Entities;

namespace Sklepix.Data.Seeds
{
    public class CategorySeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryEntity>().HasData(new List<CategoryEntity>()
            {
                new CategoryEntity()
                {
                    Id = 1,
                    Name = "Warzywa",
                },
                new CategoryEntity()
                {
                    Id = 2,
                    Name = "Owoce",
                },
                new CategoryEntity()
                {
                    Id = 3,
                    Name = "Napoje",
                },
                new CategoryEntity()
                {
                    Id = 4,
                    Name = "Pieczywo",
                }
            });
        }
    }
}
