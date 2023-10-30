using Microsoft.AspNetCore.Mvc;
using blog_test.Models;
using Microsoft.AspNetCore.Authorization;
using blog_test.Abstractions;
using blog_test.ViewModels;

namespace blog_test.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetList();
            var mappedPosts = posts.Select(post => new PostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                ChangedDate = post.ChangedDate
            }).ToList();

            return View(mappedPosts);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var post = await _postService.GetPost(id);
            if (post == null)
            {
                return NotFound();
            }
            
            var model = new PostDetailedViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                Comments = post.Comments
                .Select(c => new CommentViewModel
                {
                    Id = c.Id,
                    Content = c.Content, 
                    Author = c.Author.UserName, 
                    ChangedDate= c.ChangedDate,
                    PostId = post.Id
                }).ToList(),
            };

            return View(model);
        }
        
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(CreatePostModel model)
        {
            if (!ModelState.IsValid) 
                return View(model);
            
            await _postService.Create(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var post = await _postService.GetPost(id);
            if (post == null)
            {
                return NotFound();
            }

            return View(new UpdatePostModel
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
            });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, UpdatePostModel model)
        {
            if (!ModelState.IsValid) 
                return View(model);
            
            await _postService.Update(model);
            return RedirectToAction(nameof(Index));
        }
        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _postService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
