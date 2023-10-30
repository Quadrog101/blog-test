using System.ComponentModel.DataAnnotations;

namespace blog_test.Models
{
    public class CreateCommentModel
    {
        public int PostId { get; set; }
        
        [Display(Name = "Сообщение")]
        [MaxLength(256)]
        public string Content { get; set; }
    }
}
