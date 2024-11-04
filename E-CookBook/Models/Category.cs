using System.ComponentModel.DataAnnotations;

namespace E_CookBook.Models
{
    public class Category
    {
        public int ID { get; set; }
        [Display(Name = "Category")]
        public string? Name { get; set; }
    }
}
