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
                return BadRequest("No file uploaded.");
            }

            try
            {              
                RecipeOCR recipeOCR = new RecipeOCR();
                string result = await recipeOCR.ProcessImage(croppedIngredients);
                return RedirectToAction("CreateWithOCR", "Recipes", new { ingredients = result});
            }
            catch (Exception ex)
            {
                // Handle the exception (log it, return error response, etc.)
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        private byte[] ConvertBase64ToBytes(string base64String)
        {
            string base64Cleaned = base64String.Split(',')[1];
            base64Cleaned = base64Cleaned.Replace(" ", "+");
            return Convert.FromBase64String(base64Cleaned);
        }
    }
}
