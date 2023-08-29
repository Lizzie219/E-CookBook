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
    }
}
