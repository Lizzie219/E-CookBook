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
using X.PagedList;

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
        public IActionResult Index(string option, string searchParameter, int? pageNumber, string[] selectedCategories, string[] selectedPriceCategories, string[] selectedTags, int? selectedCookingTime)
        {
            #region Viewbags
            ViewBag.Option = option;
            ViewBag.SearchParameter = searchParameter;
            ViewBag.Categories = _context.Category.Select(c => c.Name).ToList();
            ViewBag.SelectedCategories = selectedCategories;
            ViewBag.PriceCategories = _context.PriceCategory.Select(c => c.Name).ToList();
            ViewBag.SelectedPriceCategories = selectedPriceCategories;
            List<List<string>> tags = _context.Recipe.Where(r => !string.IsNullOrEmpty(r.Tags)).Select(r => r.Tags.Split("|and|", StringSplitOptions.RemoveEmptyEntries).ToList()).ToList();
            List<string> distinctTags = new List<string>();
            foreach (var item in tags)
            {
                foreach (var tag in item)
                {
                    distinctTags.Add(tag);
                }
            }
            ViewBag.Tags = distinctTags.Distinct();
            ViewBag.SelectedTags = selectedTags;
            ViewBag.CookingTimeMax = _context.Recipe.Max(r => r.CookingTime);
            ViewBag.SelectedCookingTime = selectedCookingTime != null ? selectedCookingTime : ViewBag.CookingTimeMax;
            #endregion
            List<Recipe> recipes = _context.Recipe.Include(r => r.Category).Include(r => r.PriceCategory).ToList();
            if(selectedCategories != null && selectedCategories.Length > 0)
            {
                recipes = recipes.Where(r => selectedCategories.Any(c => r.Category != null && c == r.Category.Name)).ToList();
            }
            if (selectedPriceCategories != null && selectedPriceCategories.Length > 0)
            {
                recipes = recipes.Where(r => selectedPriceCategories.Any(c => r.PriceCategory != null && c == r.PriceCategory.Name)).ToList();
            }
            if (selectedTags != null && selectedTags.Length > 0)
            {
                recipes = recipes.Where(r => selectedTags.Any(t => !string.IsNullOrEmpty(r.Tags) && r.Tags.Contains(t))).ToList();
            }
            if(selectedCookingTime != null)
            {
                recipes = recipes.Where(r => r.CookingTime <= selectedCookingTime).ToList();
            }

            if (option == "Title")
            {
                return View(recipes.Where(r => string.IsNullOrEmpty(searchParameter) || r.Name.ToLower().Contains(searchParameter.ToLower())).ToList().ToPagedList(pageNumber ?? 1, 3));
            }
            else /*if (option == "Ingredient")*/
            {
                return View(recipes.Where(r => string.IsNullOrEmpty(searchParameter) || (r.Ingredients.Count > 0 && r.Ingredients.Any(i => i.Ingredient.Name.ToLower().Contains(searchParameter.ToLower())))).ToList().ToPagedList(pageNumber ?? 1, 3));
            }           
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
                #region PhotoLocation                
                if (Request.Form.Files.Count > 0)
                {
                    // Only one file will be uploaded
                    recipe.PhotoLocation = Request.Form.Files[0].FileName;

                    using (var fileStream = new FileStream(Path.Combine(recipePicturesDir, Request.Form.Files[0].FileName), FileMode.Create))
                    {
                        Request.Form.Files[0].CopyTo(fileStream);
                    }
                }
                #endregion

                _context.Add(recipe);
                _context.SaveChanges();

                #region Ingredients
                if (!string.IsNullOrEmpty(Request.Form["IngredientCount"]))
                {
                    string section = "Default";
                    for (int i = 1; i <= int.Parse(Request.Form["IngredientCount"]); i++)
                    {
                        if (!string.IsNullOrEmpty(Request.Form["Metric_" + i]) && !string.IsNullOrEmpty(Request.Form["MetricName_" + i]) && !string.IsNullOrEmpty(Request.Form["IngredientName_" + i]))
                        {
                            section = !string.IsNullOrEmpty(Request.Form["Section_" + i]) ? Request.Form["Section_" + i] : section;
                            ispecController.Create(recipe.ID, double.Parse(Request.Form["Metric_" + i]), Request.Form["MetricName_" + i], Request.Form["IngredientName_" + i], section);
                        }
                    }
                }
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
            ViewBag.TagList = recipe.Tags != null ? recipe.Tags.Split("|and|", StringSplitOptions.RemoveEmptyEntries).ToArray() : null;

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
                    #region Tags
                    // tags get duplicated in the Edit process, so in the following only the distinct tags are saved
                    if (!string.IsNullOrEmpty(recipe.Tags))
                    {
                        List<string> tags = recipe.Tags.Split("|and|", StringSplitOptions.RemoveEmptyEntries).Distinct().ToList();
                        recipe.Tags = "";
                        foreach (var item in tags)
                        {
                            recipe.Tags += item + "|and|";
                        }
                    }
                    #endregion
                    #region PhotoLocation                
                    if (Request.Form.Files.Count > 0)
                    {
                        // Only one file will be uploaded
                        recipe.PhotoLocation = Request.Form.Files[0].FileName;

                        using (var fileStream = new FileStream(Path.Combine(recipePicturesDir, Request.Form.Files[0].FileName), FileMode.Create))
                        {
                            Request.Form.Files[0].CopyTo(fileStream);
                        }
                    }
                    #endregion

                    _context.Update(recipe);
                    _context.SaveChanges();

                    #region Ingredients
                    if (!string.IsNullOrEmpty(Request.Form["IngredientCount"]))
                    {
                        string section = "Default";
                        for (int i = 1; i <= int.Parse(Request.Form["IngredientCount"]); i++)
                        {
                            if (!string.IsNullOrEmpty(Request.Form["Metric_" + i]) && !string.IsNullOrEmpty(Request.Form["MetricName_" + i]) && !string.IsNullOrEmpty(Request.Form["IngredientName_" + i]))
                            {
                                section = !string.IsNullOrEmpty(Request.Form["Section" + i]) ? Request.Form["Section" + i] : section;
                                ispecController.Edit(int.Parse(Request.Form["SpecIDHidden_" + i]), double.Parse(Request.Form["Metric_" + i]), Request.Form["MetricName_" + i], Request.Form["IngredientName_" + i], recipe.ID, section);
                            }
                        }
                    }
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
            ViewBag.TagList = recipe.Tags != null ? recipe.Tags.Split("|and|", StringSplitOptions.RemoveEmptyEntries).ToArray() : null;

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
