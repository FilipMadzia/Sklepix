﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sklepix.Models;

namespace Sklepix.Data
{
    public class SklepixContext : DbContext
    {
        public SklepixContext (DbContextOptions<SklepixContext> options)
            : base(options)
        {
        }

        public DbSet<Sklepix.Models.ProductEntity> ProductEntity { get; set; } = default!;

        public DbSet<Sklepix.Models.CategoryEntity>? CategoryEntity { get; set; }

        public DbSet<Sklepix.Models.AisleEntity>? AisleEntity { get; set; }

        public DbSet<Sklepix.Models.ShelfEntity>? ShelfEntity { get; set; }
    }
}
