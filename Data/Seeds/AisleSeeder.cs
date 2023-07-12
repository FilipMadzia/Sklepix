using Microsoft.EntityFrameworkCore;
using Sklepix.Data.Entities;

namespace Sklepix.Data.Seeds
{
    public class AisleSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AisleEntity>().HasData(new List<AisleEntity>()
            {
                new AisleEntity()
                {
                    Id = 1,
                    Name = "Warzywa i owoce",
                },
                new AisleEntity()
                {
                    Id = 2,
                    Name = "Napoje",
                },
                new AisleEntity()
                {
                    Id = 3,
                    Name = "Pieczywo",
                }
            });
        }
    }
}
