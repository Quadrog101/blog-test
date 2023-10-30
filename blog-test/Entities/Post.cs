using blog_test.Data;

namespace blog_test.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public AppUser Author { get; set; }
        public DateTime ChangedDate { get; set; }
    }
}
