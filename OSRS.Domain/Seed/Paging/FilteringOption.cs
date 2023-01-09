namespace OSRS.Domain.Seed.Paging
{
    public class FilteringOption
    {
        public string Field { get; set; }

        public FilteringOperator Operator { get; set; }

        public object Value { get; set; }

        public enum FilteringOperator
        {
            Contains,
            NotContains,
            LessThan,
            LessThanEqual,
            GreaterThan,
            GreaterThanEqual,
            NotEqual,
            Equal,
            StartsWith,
            EndsWith
        }
        public enum LogicalOperator{ And, Or }
    }
}
