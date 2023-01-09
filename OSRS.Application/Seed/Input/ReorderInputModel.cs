namespace OSRS.Application.Seed.Input
{
    public class SortBaseInputModel
    {
        public int FromIndex { get; set; }
        public int ToIndex { get; set; }
    }
    public class SortInputModel : SortBaseInputModel
    {
        public string Code { get; set; }
    }
}