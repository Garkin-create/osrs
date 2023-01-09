namespace OSRS.Application.Seed.Models.Output
{
    /// <summary>
    /// Configuration Model
    /// </summary>
    public class ConfigurationModel
    {
        /// <summary>
        /// Search Terms
        /// </summary>
        public string SearchTerms { get; set; } = string.Empty;
        /// <summary>
        /// Location to Show
        /// </summary>
        public string LocationToShow { get; set; } = string.Empty;
        /// <summary>
        /// Price Slider Filter
        /// </summary>
        /// <summary>
        /// Show Products Footer
        /// </summary>
        public bool ShowProductsFooter { get; set; } = false;
        /// <summary>
        /// Configuration Constructor
        /// </summary>
        public ConfigurationModel()
        {
        }
        /// <summary>
        /// Configuration Constructor
        /// </summary>
        /// <param name="priceFilter">Price Filter</param>
        public ConfigurationModel(bool showProductsFooter=false) {
            ShowProductsFooter = showProductsFooter;
        } 
    }
}