namespace E_CookBook.ViewModels
{
    public class IngredientViewModel
    {
        public double Metric { get; set; }
        public string? MetricName { get; set; }
        public string? IngredientName { get; set; }
        public int IngredientSpecificationID { get; set; }
        public string? Section { get; set; }
        public IngredientViewModel(double metric, string metricName, string ingredientName, int ingredientSpecificationID, string section)
        {
            Metric = metric;
            MetricName = metricName;
            IngredientName = ingredientName;
            IngredientSpecificationID = ingredientSpecificationID;
            Section = section;
        }
        public IngredientViewModel()
        {

        }
    }
}
