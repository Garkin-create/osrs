using System.Collections.Generic;

namespace OSRS.Core.Models.Contratos
{
    public class ArticleModelObject
    {
        public string H1Title { get; set; }
        public List<ArticleH2Object> ArticleH2s { get; set; } = new List<ArticleH2Object>();
    }
    
    public class ArticleH2Object
    {
        public string Title { get; set; }
        public List<string> H3Titles { get; set; }

        public ArticleH2Object(string title, List<string> h3s)
        {
            Title = title;
            H3Titles = h3s;
        }
    }

}