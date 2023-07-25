using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sklepix.Data.Entities;
using Sklepix.Data.Seeds;

namespace Sklepix.Data
{
	public class SklepixContext : IdentityDbContext<UserEntity>
	{
		public DbSet<ProductEntity> ProductEntity { get; set; } = default!;
		public DbSet<CategoryEntity>? CategoryEntity { get; set; }
		public DbSet<AisleEntity>? AisleEntity { get; set; }
		public DbSet<ShelfEntity>? ShelfEntity { get; set; }
		public DbSet<UserEntity>? UserEntity { get; set; }
		public DbSet<TaskEntity>? TaskEntity { get; set; }
		public DbSet<FinishedTaskEntity>? FinishedTaskEntity { get; set; }

		public SklepixContext(DbContextOptions<SklepixContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			CategorySeeder.Seed(modelBuilder);
			AisleSeeder.Seed(modelBuilder);
		}
	}
}
