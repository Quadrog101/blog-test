using blog_test.Data;

namespace blog_test.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public Post Post { get; set; }
        public AppUser Author { get; set; }
        public DateTime ChangedDate { get; set; }
    }
}
