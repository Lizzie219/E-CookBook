using System.ComponentModel.DataAnnotations;

namespace E_CookBook.Models
{
    public class Ingredient
    {
        public int ID { get; set; }
        [Display(Name = "Ingredient")]
        public string? Name { get; set; }
    }
}
