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
    public class PriceCategoriesController : Controller
    {
        private readonly TastyDbContext _context;

        public PriceCategoriesController(TastyDbContext context)
        {
            _context = context;
        }

        // GET: PriceCategories
        public async Task<IActionResult> Index()
        {
              return _context.PriceCategory != null ? 
                          View(await _context.PriceCategory.ToListAsync()) :
                          Problem("Entity set 'TastyDbContext.PriceCategory'  is null.");
        }

        // GET: PriceCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PriceCategory == null)
            {
                return NotFound();
            }

            var priceCategory = await _context.PriceCategory
                .FirstOrDefaultAsync(m => m.ID == id);
            if (priceCategory == null)
            {
                return NotFound();
            }

            return View(priceCategory);
        }

        // GET: PriceCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PriceCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description")] PriceCategory priceCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(priceCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(priceCategory);
        }

        // GET: PriceCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PriceCategory == null)
            {
                return NotFound();
            }

            var priceCategory = await _context.PriceCategory.FindAsync(id);
            if (priceCategory == null)
            {
                return NotFound();
            }
            return View(priceCategory);
        }

        // POST: PriceCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description")] PriceCategory priceCategory)
        {
            if (id != priceCategory.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(priceCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PriceCategoryExists(priceCategory.ID))
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
            return View(priceCategory);
        }

        // GET: PriceCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PriceCategory == null)
            {
                return NotFound();
            }

            var priceCategory = await _context.PriceCategory
                .FirstOrDefaultAsync(m => m.ID == id);
            if (priceCategory == null)
            {
                return NotFound();
            }

            return View(priceCategory);
        }

        // POST: PriceCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PriceCategory == null)
            {
                return Problem("Entity set 'TastyDbContext.PriceCategory'  is null.");
            }
            var priceCategory = await _context.PriceCategory.FindAsync(id);
            if (priceCategory != null)
            {
                _context.PriceCategory.Remove(priceCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PriceCategoryExists(int id)
        {
          return (_context.PriceCategory?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
