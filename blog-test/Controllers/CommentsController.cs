using blog_test.Abstractions;
using blog_test.Models;
using Microsoft.AspNetCore.Mvc;

namespace blog_test.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentModel model)
        {
            if (ModelState.IsValid)
            {
                await _commentService.Create(model);
            }
            
            return RedirectToAction("Details", "Posts", new { id = model.PostId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id, int postId)
        {
            await _commentService.Delete(id);
            return RedirectToAction("Details", "Posts", new { id = postId });
        }
    }
}
