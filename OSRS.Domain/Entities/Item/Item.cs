namespace OSRS.Domain.Entities.Item
{
    public class ItemObject : Entity<int>
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Examine { get; set; }
        public bool Members { get; set; }
        public int LowAlch { get; set; }
        public int HighAlch { get; set; }
        public int Limit { get; set; }
        public int Value { get; set; }
        public string Icon { get; set; }                                                                                                                    
    }
}
