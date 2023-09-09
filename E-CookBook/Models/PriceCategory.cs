using System.ComponentModel.DataAnnotations;

namespace E_CookBook.Models
{
    public class PriceCategory
    {
        public int ID { get; set; }
        [Display(Name = "Price Category")]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
