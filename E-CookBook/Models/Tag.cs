using System.ComponentModel.DataAnnotations;

namespace E_CookBook.Models
{
    public class Tag
    {
        public int ID { get; set; }
        [Display(Name = "Tag")]
        public string? Name { get; set; }
    }
}
