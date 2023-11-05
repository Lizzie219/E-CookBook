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
        public async Task<IActionResult> OCR(IFormFile ingredientsFile)
        {
            if (ingredientsFile == null || ingredientsFile.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            try
            {
                RecipeOCR recipeOCR = new RecipeOCR();
                string result = await recipeOCR.ProcessImage(ingredientsFile);
                return RedirectToAction("CreateWithOCR", "Recipes", new { ingredients = result});
            }
            catch (Exception ex)
            {
                // Handle the exception (log it, return error response, etc.)
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
