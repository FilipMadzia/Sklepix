using System;
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

        public DbSet<Sklepix.Models.Product> Product { get; set; } = default!;
    }
}
