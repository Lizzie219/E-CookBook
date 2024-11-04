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
        public DbSet<E_CookBook.Models.IngredientSpecification> IngredientSpecification { get; set; } = default!;
        public DbSet<E_CookBook.Models.Recipe> Recipe { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { ID = 1, Name = "Soup" },
                new Category { ID = 2, Name = "Appetizer" },
                new Category { ID = 3, Name = "Main Course" },
                new Category { ID = 4, Name = "Salad" },
                new Category { ID = 5, Name = "Drink" },
                new Category { ID = 6, Name = "Dessert" },
                new Category { ID = 7, Name = "Side Dish" });

            modelBuilder.Entity<PriceCategory>().HasData(
                new PriceCategory { ID = 1, Name = "Inexpensive", Description = "The total cost of the ingredients is below 3000 Ft." },
                new PriceCategory { ID = 2, Name = "Moderate", Description = "The total cost of the ingredients is between 3001 and 6000 Ft." },
                new PriceCategory { ID = 3, Name = "Expensive", Description = "The total cost of the ingredients is over 6001 Ft." }
                );

        }
    }
}
