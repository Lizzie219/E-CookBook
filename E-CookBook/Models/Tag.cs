using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CookBook.Models
{
    public class Tag
    {
        public int ID { get; set; }
        [Display(Name = "Tag")]
        public string? Name { get; set; }

        public virtual ICollection<RecipeTag> Recipes { get; set; }

        public Tag()
        {
            Recipes = new HashSet<RecipeTag>();
        }
    }
}
