﻿using DotnetAPITest.Dtos.Comment;
using DotnetAPITest.Interfaces;
using DotnetAPITest.Mappers;
using DotnetAPITest.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPITest.Controllers
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
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetAll()
        {
            var comments = await _commentRepo.GetAllAsync();

            var commentDto = comments.Select(x => x.ToCommentDto());

            return Ok(commentDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDto>> GetById([FromRoute] int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);

            if (comment is null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentDto commentDto)
        {
            if (!await _stockRepo.StockExists(stockId))
            {
                return BadRequest("Stock does not exist");
            }

            var commentModel = commentDto.ToCommentFromCreate(stockId);
            await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel }, commentModel.ToCommentDto());
        }
    }
}
