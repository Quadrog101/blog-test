namespace blog_test.Models
{
    public class PostDetailedViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }
}
