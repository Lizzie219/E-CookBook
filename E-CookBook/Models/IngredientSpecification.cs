using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CookBook.Models
{
    public class IngredientSpecification
    {
        public int ID { get; set; }

        [Display(Name="Quantity")]
        public double Quantity { get; set; }

        [Display(Name = "Section")]
        public string? Section { get; set; }

        public int IngredientID { get; set; }
        [ForeignKey("IngredientID")]
        public virtual Ingredient? Ingredient { get; set; }

        public int QuantityMetricID { get; set; }
        [ForeignKey("QuantityMetricID")]
        public virtual QuantityMetric? QuantityMetric { get; set; }

        public int RecipeID { get; set; }
        [ForeignKey("RecipeID")]
        public virtual Recipe? Recipe { get; set; }
    }
}
