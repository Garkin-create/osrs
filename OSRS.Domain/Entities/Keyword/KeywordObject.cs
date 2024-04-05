using System;
using OSRS.Core.Models.Enums;
using OSRS.Domain.Entities.Project;

namespace OSRS.Domain.Entities
{
    public class KeywordObject: Entity<long>
    {
        public long ProjectId { get; set; }
        public virtual ProjectObject Project { get; set; }
        public string Word { get; set; }
        public DeviceTypeEnum Device { get; set; }
    }
}