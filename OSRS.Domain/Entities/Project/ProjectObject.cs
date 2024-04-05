using System.Collections.Generic;

namespace OSRS.Domain.Entities.Project
{
    public class ProjectObject: Entity <long>
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public bool Default { get; set; }
        public ICollection<KeywordObject> Keyworks { get; set; }
    }
}