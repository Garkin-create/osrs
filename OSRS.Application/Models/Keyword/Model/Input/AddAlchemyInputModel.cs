using OSRS.Core.Models.Enums;

namespace OSRS.Application.Models.Alchemy.Model.Input
{
    public class AddKeywordInputModel
    {
        public long ProjectId { get; set; }
        public string Word { get; set; }
        public DeviceTypeEnum Device { get; set; }
    }
}