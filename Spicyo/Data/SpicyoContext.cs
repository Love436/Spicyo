using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spicyo.Models;

namespace Spicyo.Data
{
    public class SpicyoContext : DbContext
    {
        public SpicyoContext (DbContextOptions<SpicyoContext> options)
            : base(options)
        {
        }
        public DbSet<Recipes> Recipes { get; set; }
        public DbSet<OrderedRecipes> OrderedRecipes { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Authenticate> Authenticate { get; set; }
    }
}
