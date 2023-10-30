using System.ComponentModel.DataAnnotations;

namespace blog_test.Models
{
    public class CreatePostModel
    {
        [Display(Name = "Заголовок")]
        public string Title { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
    }
}
