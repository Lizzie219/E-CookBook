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
    public class IngredientSpecificationsController : Controller
    {
        private readonly TastyDbContext _context;
        private QuantityMetricsController quantityMetricsController;
        private IngredientsController ingredientsController;

        public IngredientSpecificationsController(TastyDbContext context)
        {
            _context = context;
            quantityMetricsController = new QuantityMetricsController(context);
            ingredientsController = new IngredientsController(context);
        }

        // GET: IngredientSpecifications
        public async Task<IActionResult> Index()
        {
            var tastyDbContext = _context.IngredientSpecification.Include(i => i.Ingredient).Include(i => i.QuantityMetric).Include(i => i.Recipe);
            return View(await tastyDbContext.ToListAsync());
        }

        // GET: IngredientSpecifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.IngredientSpecification == null)
            {
                return NotFound();
            }

            var ingredientSpecification = await _context.IngredientSpecification
                .Include(i => i.Ingredient)
                .Include(i => i.QuantityMetric)
                .Include(i => i.Recipe)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ingredientSpecification == null)
            {
                return NotFound();
            }

            return View(ingredientSpecification);
        }

        // GET: IngredientSpecifications/Create
        public IActionResult Create()
        {
            ViewData["IngredientID"] = new SelectList(_context.Ingredient, "ID", "ID");
            ViewData["QuantityMetricID"] = new SelectList(_context.QuantityMetric, "ID", "ID");
            ViewData["RecipeID"] = new SelectList(_context.Recipe, "ID", "ID");
            return View();
        }

        // POST: IngredientSpecifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Quantity,IngredientID,QuantityMetricID,RecipeID")] IngredientSpecification ingredientSpecification)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ingredientSpecification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IngredientID"] = new SelectList(_context.Ingredient, "ID", "ID", ingredientSpecification.IngredientID);
            ViewData["QuantityMetricID"] = new SelectList(_context.QuantityMetric, "ID", "ID", ingredientSpecification.QuantityMetricID);
            ViewData["RecipeID"] = new SelectList(_context.Recipe, "ID", "ID", ingredientSpecification.RecipeID);
            return View(ingredientSpecification);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task Create(int recipeID, double metric, string metricName, string ingredientName)
        {
            IngredientSpecification ingredientSpecification = new IngredientSpecification();
            ingredientSpecification.RecipeID = recipeID;
            ingredientSpecification.Quantity = metric;

            #region Quantity Metric

            // if the new metric is already in the database it will not be duplicated
            await quantityMetricsController.Create(metricName);
            ingredientSpecification.QuantityMetricID = quantityMetricsController.GetQuantityMetric(metricName);

            #endregion
            #region Ingredient

            // if the new ingredient is already in the database it will not be duplicated
            await ingredientsController.Create(ingredientName);
            ingredientSpecification.IngredientID = ingredientsController.GetIngredient(ingredientName);

            #endregion

            if(!IngredientSpecificationExists(ingredientSpecification))
            {
                _context.IngredientSpecification.Add(ingredientSpecification);
                await _context.SaveChangesAsync();
            }
        }

        // GET: IngredientSpecifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.IngredientSpecification == null)
            {
                return NotFound();
            }

            var ingredientSpecification = await _context.IngredientSpecification.FindAsync(id);
            if (ingredientSpecification == null)
            {
                return NotFound();
            }
            ViewData["IngredientID"] = new SelectList(_context.Ingredient, "ID", "ID", ingredientSpecification.IngredientID);
            ViewData["QuantityMetricID"] = new SelectList(_context.QuantityMetric, "ID", "ID", ingredientSpecification.QuantityMetricID);
            ViewData["RecipeID"] = new SelectList(_context.Recipe, "ID", "ID", ingredientSpecification.RecipeID);
            return View(ingredientSpecification);
        }

        // POST: IngredientSpecifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Quantity,IngredientID,QuantityMetricID,RecipeID")] IngredientSpecification ingredientSpecification)
        {
            if (id != ingredientSpecification.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingredientSpecification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredientSpecificationExists(ingredientSpecification.ID))
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
            ViewData["IngredientID"] = new SelectList(_context.Ingredient, "ID", "ID", ingredientSpecification.IngredientID);
            ViewData["QuantityMetricID"] = new SelectList(_context.QuantityMetric, "ID", "ID", ingredientSpecification.QuantityMetricID);
            ViewData["RecipeID"] = new SelectList(_context.Recipe, "ID", "ID", ingredientSpecification.RecipeID);
            return View(ingredientSpecification);
        }

        // GET: IngredientSpecifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.IngredientSpecification == null)
            {
                return NotFound();
            }

            var ingredientSpecification = await _context.IngredientSpecification
                .Include(i => i.Ingredient)
                .Include(i => i.QuantityMetric)
                .Include(i => i.Recipe)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ingredientSpecification == null)
            {
                return NotFound();
            }

            return View(ingredientSpecification);
        }

        // POST: IngredientSpecifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.IngredientSpecification == null)
            {
                return Problem("Entity set 'TastyDbContext.IngredientSpecification'  is null.");
            }
            var ingredientSpecification = await _context.IngredientSpecification.FindAsync(id);
            if (ingredientSpecification != null)
            {
                _context.IngredientSpecification.Remove(ingredientSpecification);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngredientSpecificationExists(int id)
        {
            return (_context.IngredientSpecification?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        private bool IngredientSpecificationExists(IngredientSpecification ingredientSpecification)
        {
            return _context.IngredientSpecification.Where(i => i.RecipeID == ingredientSpecification.RecipeID &&
                                                               i.IngredientID == ingredientSpecification.IngredientID &&
                                                               double.Equals(i.Quantity, ingredientSpecification.Quantity) &&
                                                               i.QuantityMetricID == ingredientSpecification.QuantityMetricID).FirstOrDefault() != null;
        }
    }
}
