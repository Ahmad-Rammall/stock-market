﻿using Stock_Market_API.DTOs.Comment;
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

        public static Comment ToCommentFromCreateDTO(CreateCommentDTO commentModel, int stockId)
        {
            return new Comment
            {
                Title = commentModel.Title,
                Content = commentModel.Content,
                StockId = stockId,
            };
        }
        public static Comment ToCommentFromUpdateDTO(UpdateCommentDTO commentModel)
        {
            return new Comment
            {
                Title = commentModel.Title,
                Content = commentModel.Content,
            };
        }
    }
}
