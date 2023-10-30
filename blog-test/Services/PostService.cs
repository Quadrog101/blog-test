using blog_test.Abstractions;
using blog_test.Data;
using blog_test.Entities;
using blog_test.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace blog_test.Services
{
    public class PostService : IPostService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public PostService(AppDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<List<Post>> GetList()
        {
            return await _context.Posts
                .OrderByDescending(x => x.ChangedDate)
                .ToListAsync();
        }

        public async Task<Post?> GetPost(int postId)
        {
            return await _context.Posts
                .Include(p => p.Comments)
                .ThenInclude(c => c.Author)
                .FirstOrDefaultAsync(m => m.Id == postId);
        }
        
        public async Task Create(CreatePostModel model)
        {
            var user = await GetUser();
            if (user == null)
                return;
            
            _context.Add(new Post
            {
                Title = model.Title,
                Description = model.Description,
                ChangedDate = DateTime.Now,
                Author = user
            });
            await _context.SaveChangesAsync();
        }

        public async Task Update(UpdatePostModel model)
        {
            var user = await GetUser();
            if (user == null)
                return;
            
            var post = await _context.Posts
                .Include(post => post.Author)
                .FirstOrDefaultAsync(post => post.Id == model.Id);
            if (post == null)
                return;
            
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            if (isAdmin || post.Author.Id == user.Id)
            {
                post.Title = model.Title;
                post.Description = model.Description;
                post.ChangedDate = DateTime.UtcNow;

                _context.Update(post);
                await _context.SaveChangesAsync();
            }
        }
        
        public async Task Delete(int id)
        {
            var user = await GetUser();
            if (user == null)
                return;
            
            var post = await _context.Posts
                .Include(post => post.Author)
                .FirstOrDefaultAsync(post => post.Id == id);
            if (post == null)
                return;
            
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            if (isAdmin || post.Author.Id == user.Id)
            {
                _context.Remove(post);
                await _context.SaveChangesAsync();
            }
        }

        private async Task<AppUser?> GetUser()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null) 
                return null;
            var user = await _userManager.GetUserAsync(httpContext.User);
            return user;
        }
    }
}
