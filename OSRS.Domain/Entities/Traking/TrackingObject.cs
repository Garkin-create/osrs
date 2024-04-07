using System;
using System.Security.Principal;
using OSRS.Domain.Entities.Project;

namespace OSRS.Domain.Entities.Traking
{
    public class TrackingObject: Entity <long>
    {
        public long KeywordId { get; set; }
        public virtual KeywordObject Keyword { get; set; }
        public int Position { get; set; }
        public DateTime Date { get; set; }
    }
}