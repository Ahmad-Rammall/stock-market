using Microsoft.AspNetCore.Mvc;
using Stock_Market_API.Interfaces;
using Stock_Market_API.Mappers;

namespace Stock_Market_API.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        public CommentController(ICommentRepository commentRepo)
        {
            _commentRepo = commentRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepo.GetAllAsync();
            var commentDTO = comments.Select(s => CommentMappers.ToCommentDTO(s));

            return Ok(commentDTO);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null) { return NotFound(); }

            return Ok(CommentMappers.ToCommentDTO(comment));
        }
    }
}
