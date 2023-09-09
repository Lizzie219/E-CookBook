using System.ComponentModel.DataAnnotations.Schema;

namespace E_CookBook.Models
{
    public class RecipeTag
    {
        public int ID { get; set; }

        public int TagID { get; set; }
        [ForeignKey("TagID")]
        public virtual Tag? Tag { get; set; }

        public int RecipeID { get; set; }
        [ForeignKey("RecipeID")]
        public virtual Recipe? Recipe { get; set; }
    }
}
