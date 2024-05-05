namespace OSRS.Application.Models.WordPress.Model.Input
{
    public class PostListInputModel
    {
        public string Url { get; set; }
        public int Page { get; set; }
        public int ItemPerPage { get; set; }
    }
}