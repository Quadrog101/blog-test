using blog_test.Models;

namespace blog_test.Abstractions
{
    public interface ICommentService
    {
        public Task Create(CreateCommentModel model);
        Task Delete(int id);
    }
}