namespace OSRS.Core.Models.Contratos
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public string Dscription { get; set; }
        public string Link { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Taxonomy { get; set; }
        public int Parent { get; set; }
    }
}