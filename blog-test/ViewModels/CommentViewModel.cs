using System.ComponentModel.DataAnnotations;

namespace blog_test.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        
        [Required]
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime ChangedDate { get; set; }
        
        public int PostId { get; set; }
    }
}
