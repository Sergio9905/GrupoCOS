using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaGrupoCOS.Application.DTOs;
using PruebaTecnicaGrupoCOS.Application.Interfaces;
using PruebaTecnicaGrupoCOS.Application.Responses;
using PruebaTecnicaGrupoCOS.Core.Entities;
using PruebaTecnicaGrupoCOS.Infrastructure.Data;

namespace PruebaTecnicaGrupoCOS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;
        private readonly ILogger<CommentsController> _logger;

        public CommentsController(ICommentService commentService,
                                 IMapper mapper,
                                 ILogger<CommentsController> logger)
        {
            _commentService = commentService;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize]
        [HttpGet("get-comments")]
        public async Task<ActionResult<IEnumerable<CommentResponse>>> GetComments()
        {
            try
            {
                var comments = await _commentService.GetAllCommentsAsync();
                return Ok(comments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all comments");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-comment-by-id/{id}")]
        public async Task<ActionResult<CommentResponse>> GetComment(int id)
        {
            try
            {
                var comment = await _commentService.GetCommentByIdAsync(id);
                return Ok(comment);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting comment with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-comments-by-post/{postId}")]
        public async Task<ActionResult<IEnumerable<CommentResponse>>> GetCommentsByPost(int postId)
        {
            try
            {
                var comments = await _commentService.GetCommentsByPostIdAsync(postId);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting comments for post {postId}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPost("create-comment")]
        public async Task<ActionResult<CommentResponse>> CreateComment(CommentDto commentDto)
        {
            try
            {
                var result = await _commentService.CreateCommentAsync(commentDto);
                return CreatedAtAction(nameof(GetComment), new { id = result.Id }, result);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating comment");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPut("update-comment-by-id/{id}")]
        public async Task<IActionResult> UpdateComment(int id, CommentDto commentDto)
        {
            if (id != commentDto.Id)
                return BadRequest("ID mismatch");

            try
            {
                var result = await _commentService.UpdateCommentAsync(id, commentDto);
                if (!result) return BadRequest("Update failed");
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating comment with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpDelete("delete-comment-by-id/{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            try
            {
                var result = await _commentService.DeleteCommentAsync(id);
                if (!result) return BadRequest("Delete failed");
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting comment with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
