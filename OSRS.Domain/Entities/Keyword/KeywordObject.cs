using System;
using System.Collections.Generic;
using OSRS.Core.Models.Enums;
using OSRS.Domain.Entities.Project;
using OSRS.Domain.Entities.Traking;

namespace OSRS.Domain.Entities
{
    public class KeywordObject: Entity<long>
    {
        public long ProjectId { get; set; }
        public virtual ProjectObject Project { get; set; }
        public string Word { get; set; }
        public DeviceTypeEnum Device { get; set; }
        public ICollection<TrackingObject> Trackings { get; set; }
    }
}