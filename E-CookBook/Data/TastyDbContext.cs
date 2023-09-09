using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using E_CookBook.Models;

namespace E_CookBook.Data
{
    public class TastyDbContext : DbContext
    {
        public TastyDbContext (DbContextOptions<TastyDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<E_CookBook.Models.Ingredient> Ingredient { get; set; } = default!;
        public DbSet<E_CookBook.Models.Category> Category { get; set; } = default!;
        public DbSet<E_CookBook.Models.PriceCategory> PriceCategory { get; set; } = default!;
        public DbSet<E_CookBook.Models.QuantityMetric> QuantityMetric { get; set; } = default!;
        public DbSet<E_CookBook.Models.Tag> Tag { get; set; } = default!;
        public DbSet<E_CookBook.Models.RecipeTag> RecipeTag { get; set; } = default!;
        public DbSet<E_CookBook.Models.IngredientSpecification> IngredientSpecification { get; set; } = default!;
        public DbSet<E_CookBook.Models.Recipe> Recipe { get; set; } = default!;

    }
}
