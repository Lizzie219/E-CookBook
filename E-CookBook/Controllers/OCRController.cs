using E_CookBook.OCR;
using Microsoft.AspNetCore.Mvc;

namespace E_CookBook.Controllers
{
    public class OCRController : Controller
    {
        public IActionResult OCR()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessImages(IFormFile croppedIngredients, IFormFile croppedInstructions)
        {
            if (croppedIngredients == null && croppedInstructions == null)
            {
                return RedirectToAction("ErrorOCR", "OCR", new { errorMessage = "No file uploaded" });
            }
            try
            {
                RecipeOCR recipeOCR = new RecipeOCR();
                string ingredients = "";
                string instructions = "";
                if (croppedIngredients != null)
                {
                    ingredients = await recipeOCR.ProcessImage(croppedIngredients);
                }
                if (croppedInstructions != null)
                {
                    instructions = await recipeOCR.ProcessImage(croppedInstructions);
                }
                return RedirectToAction("CreateWithOCR", "Recipes", new { ingredients = ingredients, instructions = instructions });
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorOCR", "OCR", new { errorMessage = "500 - " + ex.Message });
            }
        }
        public IActionResult ErrorOCR(string errorMessage)
        {
            ViewBag.ErrorMessage = errorMessage;
            return View();
        }
    }
}
