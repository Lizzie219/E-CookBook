using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_CookBook.Data;
using E_CookBook.Models;

namespace E_CookBook.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly TastyDbContext _context;

        public IngredientsController(TastyDbContext context)
        {
            _context = context;
        }

        // GET: Ingredients
        public async Task<IActionResult> Index()
        {
              return _context.Ingredient != null ? 
                          View(await _context.Ingredient.ToListAsync()) :
                          Problem("Entity set 'TastyDbContext.Ingredient'  is null.");
        }

        // GET: Ingredients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ingredient == null)
            {
                return NotFound();
            }

            var ingredient = await _context.Ingredient
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ingredient == null)
            {
                return NotFound();
            }

            return View(ingredient);
        }

        // GET: Ingredients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ingredients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ingredient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ingredient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task Create(string name)
        {
            if (!IngredientExists(name))
            {
                Ingredient ingredient = new Ingredient();
                ingredient.Name = name;

                _context.Ingredient.Add(ingredient);
                await _context.SaveChangesAsync();
            }
        }

        // GET: Ingredients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ingredient == null)
            {
                return NotFound();
            }

            var ingredient = await _context.Ingredient.FindAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }
            return View(ingredient);
        }

        // POST: Ingredients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] Ingredient ingredient)
        {
            if (id != ingredient.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingredient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredientExists(ingredient.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ingredient);
        }

        // GET: Ingredients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ingredient == null)
            {
                return NotFound();
            }

            var ingredient = await _context.Ingredient
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ingredient == null)
            {
                return NotFound();
            }

            return View(ingredient);
        }

        // POST: Ingredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ingredient == null)
            {
                return Problem("Entity set 'TastyDbContext.Ingredient'  is null.");
            }
            var ingredient = await _context.Ingredient.FindAsync(id);
            if (ingredient != null)
            {
                _context.Ingredient.Remove(ingredient);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public int GetIngredient(string name)
        {
            return _context.Ingredient.Where(q => q.Name == name).Select(q => q.ID).FirstOrDefault();
        }

        private bool IngredientExists(int id)
        {
          return (_context.Ingredient?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        private bool IngredientExists(string name)
        {
            return (_context.Ingredient?.Any(e => e.Name == name)).GetValueOrDefault();
        }
    }
}
