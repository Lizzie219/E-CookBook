using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CookBook.Models
{
    public class Recipe
    {
        public int ID { get; set; }

        [Display(Name="Recipe")]
        public string? Name { get; set; }

        [Display(Name = "Photo Location")]
        public string? PhotoLocation { get; set; }

        [Display(Name = "Cooking Time")]
        public int CookingTime { get; set; }

        [Display(Name = "Portion")]
        public int Portion { get; set; }

        [Display(Name = "Instructions")]
        public string? Instructions { get; set; }

        [Display(Name="Source of the Recipe")]
        public string? Source { get; set; }

        public virtual ICollection<IngredientSpecification> Ingredients { get; set; }

        public virtual ICollection<RecipeTag> Tags { get; set; }

        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public virtual Category? Category { get; set; }

        public int PriceCategoryID { get; set; }
        [ForeignKey("PriceCategoryID")]
        public virtual PriceCategory? PriceCategory { get; set; }

        public Recipe()
        {
            Ingredients = new HashSet<IngredientSpecification>();
            Tags = new HashSet<RecipeTag>();
        }
    }
}
