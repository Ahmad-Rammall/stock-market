using Stock_Market_API.Models;

namespace Stock_Market_API.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment> GetByIdAsync(int id);
        Task<Comment> CreateCommentAsync(Comment commentModel);
        Task<Comment> UpdateCommentAsync(int commentId, Comment commentModel);
        Task<Comment> DeleteCommentAsync(int commentId);
    }
}
