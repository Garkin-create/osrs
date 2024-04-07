using System;
using OSRS.Core.Models.Contratos;

namespace OSRS.Application.Models.WordPress.Model.Output
{
    public class PostListOutputModel
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public DateTime date_gmt { get; set; }
        public GuidObject guid { get; set; }
        public DateTime modified { get; set; }
        public DateTime modified_gmt { get; set; }
        public string slug { get; set; }
        public string status { get; set; }
        public string type { get; set; }
        public string link { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        
    }
}