using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sklepix.Data.Entities;

namespace Sklepix.Data
{
    public class SklepixContext : DbContext
    {
        public SklepixContext (DbContextOptions<SklepixContext> options)
            : base(options)
        {
        }

        public DbSet<ProductEntity> ProductEntity { get; set; } = default!;

        public DbSet<CategoryEntity>? CategoryEntity { get; set; }

        public DbSet<AisleEntity>? AisleEntity { get; set; }

        public DbSet<ShelfEntity>? ShelfEntity { get; set; }
    }
}
