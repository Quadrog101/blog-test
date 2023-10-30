using System.ComponentModel.DataAnnotations;

namespace blog_test.Models
{
    public class UpdatePostModel
    {
        public int Id { get; set; }
        [Display(Name = "Заголовок")]
        public string Title { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
    }
}