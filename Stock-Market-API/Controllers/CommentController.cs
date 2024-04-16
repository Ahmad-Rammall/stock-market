using Microsoft.AspNetCore.Mvc;
using Stock_Market_API.DTOs.Comment;
using Stock_Market_API.Interfaces;
using Stock_Market_API.Mappers;

namespace Stock_Market_API.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
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
        [HttpPost("{stockId}")]
        public async Task<IActionResult> CreateComment([FromRoute] int stockId, CreateCommentDTO commentDTO)
        {
            if (!await _stockRepo.StockExists(stockId)) { return BadRequest("Stock Doesnt Exist!"); }

            var commentModel = CommentMappers.ToCommentFromCreateDTO(commentDTO, stockId);
            await _commentRepo.CreateCommentAsync(commentModel);

            return CreatedAtAction(nameof(GetCommentById), new {id = commentModel}, CommentMappers.ToCommentDTO(commentModel));
        }
    }
}
