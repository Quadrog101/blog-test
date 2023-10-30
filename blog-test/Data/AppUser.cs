using blog_test.Entities;
using Microsoft.AspNetCore.Identity;

namespace blog_test.Data
{
    public class AppUser : IdentityUser
    {
        public List<Post> Posts { get; set; }
        public List<Comment> Comments { get; set; }
    }
}

