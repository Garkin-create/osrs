using System;

namespace OSRS.Core.Models.Contratos
{
    public class PostModel
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
        public TitleObject title { get; set; }
        public ContentObject content { get; set; }
    }
    public class GuidObject
    {
        public string rendered { get; set; }
    }

    public class TitleObject
    {
        public string rendered { get; set; }
    }

    public class ContentObject
    {
        public string rendered { get; set; }
        public bool @protected { get; set; }
    }


}