using Stock_Market_API.DTOs.Comment;
using Stock_Market_API.Models;

namespace Stock_Market_API.Mappers
{
    public class CommentMappers
    {
        public static CommentDTO ToCommentDTO(Comment commentModel)
        {
            return new CommentDTO
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedAt = commentModel.CreatedAt,
                StockId = commentModel.StockId,
            };
        }
    }
}
