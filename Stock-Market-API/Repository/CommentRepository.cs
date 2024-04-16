using Microsoft.EntityFrameworkCore;
using Stock_Market_API.Data;
using Stock_Market_API.Interfaces;
using Stock_Market_API.Models;

namespace Stock_Market_API.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        async Task<List<Comment>> ICommentRepository.GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }
    }
}
