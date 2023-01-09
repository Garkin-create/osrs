
using OSRS.Domain.Seed.Paging;

namespace OSRS.Application.Seed.Input
{
    public class FilteringOptionInput
    {
        public string Field { get; set; }

        public FilteringOption.FilteringOperator Operator { get; set; }

        public string Value { get; set; }
    }
}
