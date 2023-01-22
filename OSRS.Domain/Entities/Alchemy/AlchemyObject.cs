namespace OSRS.Domain.Entities
{
    public class AlchemyObject : Entity <long>
    {
        public int ItemId { get; set; }
        public int ItemName { get; set; }
        public int ItemCant { get; set; }
        public int ItemPrice { get; set; }
        public int NaturePrice { get; set; }
        public int ItemHighAlchemy { get; set; }
    }
}