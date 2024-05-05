using System;

namespace OSRS.Domain.Entities.Post
{
    public class PostObject: Entity<long>
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsNew { get; set; }
    }
}