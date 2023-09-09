using System.ComponentModel.DataAnnotations;

namespace E_CookBook.Models
{
    public class QuantityMetric
    {
        public int ID { get; set; }
        [Display(Name = "Metric")]
        public string? Name { get; set; }
    }
}
