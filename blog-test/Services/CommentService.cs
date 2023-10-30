using blog_test.Abstractions;
using blog_test.Data;
using blog_test.Entities;
using blog_test.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace blog_test.Services
{
    public class CommentService : ICommentService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommentService(AppDbContext context, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public async Task Create(CreateCommentModel model)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == model.PostId);
            var userClaims = _httpContextAccessor.HttpContext?.User;
            var user = await _userManager.GetUserAsync(userClaims);

            if (post == null || user == null)
            {
                return;
            }
            
            post.Comments.Add(new Comment
            {
                Content = model.Content,
                Author = user,
                ChangedDate = DateTime.Now
            });
            _context.Update(post);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var comment = await _context.Comments
                .Include(c => c.Author)
                .FirstOrDefaultAsync(c => c.Id == id);
            var userClaims = _httpContextAccessor.HttpContext?.User;
            var user = await _userManager.GetUserAsync(userClaims);
            
            if (comment == null || user == null)
            {
                return;
            }
            
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            if (comment.Author.Id == user.Id || isAdmin)
            {
                _context.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }
    }
}