using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_CookBook.Data;
using E_CookBook.ViewModels;
using E_CookBook.Models;

namespace E_CookBook.Controllers
{
    public class RecipesController : Controller
    {
        private readonly TastyDbContext _context;
        private IngredientSpecificationsController ispecController;
        private readonly string recipePicturesDir = Path.Combine(Directory.GetCurrentDirectory(), "RecipePictures");

        public RecipesController(TastyDbContext context)
        {
            _context = context;
            ispecController = new IngredientSpecificationsController(_context);
        }

        // GET: Recipes
        public async Task<IActionResult> Index()
        {
            var tastyDbContext = _context.Recipe.Include(r => r.Category).Include(r => r.PriceCategory);
            return View(await tastyDbContext.ToListAsync());
        }

        // GET: Recipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Recipe == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe
                .Include(r => r.Category)
                .Include(r => r.PriceCategory)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // GET: Recipes/Create
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Category, "ID", "Name");
            ViewBag.PriceCategories = new SelectList(_context.PriceCategory, "ID", "Name");

            return View();
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ID,Name,CookingTime,Portion,Instructions,Source,Tags,CategoryID,PriceCategoryID")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipe);
                _context.SaveChanges();

                #region Ingredients
                if (!string.IsNullOrEmpty(Request.Form["IngredientCount"]))
                {
                    for (int i = 1; i <= int.Parse(Request.Form["IngredientCount"]); i++)
                    {                      
                        if (!string.IsNullOrEmpty(Request.Form["Metric_" + i]) && !string.IsNullOrEmpty(Request.Form["MetricName_" + i]) && !string.IsNullOrEmpty(Request.Form["IngredientName_" + i]))
                        {
                            ispecController.Create(recipe.ID, double.Parse(Request.Form["Metric_" + i]), Request.Form["MetricName_" + i], Request.Form["IngredientName_" + i]);
                        }
                    }
                }
                #endregion
                #region PhotoLocation                
                

                #endregion

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = new SelectList(_context.Category, "ID", "Name");
            ViewBag.PriceCategories = new SelectList(_context.PriceCategory, "ID", "Name");
            return View(recipe);
        }

        // GET: Recipes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.Recipe == null)
            {
                return NotFound();
            }

            var recipe = _context.Recipe.Find(id);
            if (recipe == null)
            {
                return NotFound();
            }

            List<IngredientViewModel> ingredients = new List<IngredientViewModel>();
            foreach (var ingredient in recipe.Ingredients)
            {
                ingredients.Add(new IngredientViewModel(ingredient.Quantity, ingredient.QuantityMetric.Name, ingredient.Ingredient.Name, ingredient.ID));
            }

            ViewBag.ExistingIngredients = ingredients;
            ViewBag.Categories = new SelectList(_context.Category, "ID", "Name");
            ViewBag.PriceCategories = new SelectList(_context.PriceCategory, "ID", "Name");
            ViewBag.TagList = recipe.Tags != null ? recipe.Tags.Split("|and|").SkipLast(1).ToArray() : null;

            return View(recipe);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,Name,CookingTime,Portion,Instructions,Source,Tags,CategoryID,PriceCategoryID")] Recipe recipe)
        {
            if (id != recipe.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipe);
                    _context.SaveChanges();

                    #region Ingredients
                    if (!string.IsNullOrEmpty(Request.Form["IngredientCount"]))
                    {
                        for (int i = 1; i <= int.Parse(Request.Form["IngredientCount"]); i++)
                        {
                            if (!string.IsNullOrEmpty(Request.Form["Metric_" + i]) && !string.IsNullOrEmpty(Request.Form["MetricName_" + i]) && !string.IsNullOrEmpty(Request.Form["IngredientName_" + i]))
                            {
                                ispecController.Edit(int.Parse(Request.Form["SpecIDHidden_" + i]), double.Parse(Request.Form["Metric_" + i]), Request.Form["MetricName_" + i], Request.Form["IngredientName_" + i], recipe.ID);
                            }
                        }
                    }
                    #endregion
                    #region PhotoLocation                


                    #endregion


                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(recipe.ID))
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

            List<IngredientViewModel> ingredients = new List<IngredientViewModel>();
            List<IngredientSpecification> ingredientsofCurrentRecipe = _context.IngredientSpecification.Where(s => s.RecipeID == id).ToList();
            foreach (var ingredient in ingredientsofCurrentRecipe)
            {
                ingredients.Add(new IngredientViewModel(ingredient.Quantity, ingredient.QuantityMetric.Name, ingredient.Ingredient.Name, ingredient.ID));
            }

            ViewBag.ExistingIngredients = ingredients;
            ViewBag.Categories = new SelectList(_context.Category, "ID", "Name");
            ViewBag.PriceCategories = new SelectList(_context.PriceCategory, "ID", "Name");
            ViewBag.TagList = recipe.Tags != null ? recipe.Tags.Split("|and|").SkipLast(1).ToArray() : null;

            return View(recipe);
        }

        // GET: Recipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Recipe == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe
                .Include(r => r.Category)
                .Include(r => r.PriceCategory)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Recipe == null)
            {
                return Problem("Entity set 'TastyDbContext.Recipe'  is null.");
            }
            var recipe = await _context.Recipe.FindAsync(id);
            if (recipe != null)
            {
                _context.Recipe.Remove(recipe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeExists(int id)
        {
            return (_context.Recipe?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
