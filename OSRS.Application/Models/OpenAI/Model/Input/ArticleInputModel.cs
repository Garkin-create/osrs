using System.Collections.Generic;

namespace OSRS.Application.Models.OpenAI.Model.Input
{
    public class ArticleInputModel
    {
        public string Title { get; set; }
        public List<ArticleH1> ArticleH1 { get; set; }
    }

    public class ArticleH1
    {
        public string Title { get; set; }
        public ArticleH2 ArticleH2 { get; set; }
    }
    public class ArticleH2
    {
        public List<string> Titles { get; set; }
    }
}