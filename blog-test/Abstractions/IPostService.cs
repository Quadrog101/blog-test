using blog_test.Entities;
using blog_test.Models;

namespace blog_test.Abstractions
{
    public interface IPostService
    {
        Task Create(CreatePostModel model);
        Task Update(UpdatePostModel model);
        Task<Post?> GetPost(int postId);
        Task<List<Post>> GetList();
        Task Delete(int id);
    }
}
