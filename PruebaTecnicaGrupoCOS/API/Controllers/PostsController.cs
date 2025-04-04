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
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly ILogger<PostsController> _logger;

        public PostsController(IPostService postService,
                              IMapper mapper,
                              ILogger<PostsController> logger)
        {
            _postService = postService;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize]
        [HttpGet("get-posts")]
        public async Task<ActionResult<IEnumerable<PostResponse>>> GetPosts()
        {
            try
            {
                var posts = await _postService.GetAllPostsAsync();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all posts");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-post-by-id/{id}")]
        public async Task<ActionResult<PostResponse>> GetPost(int id)
        {
            try
            {
                var post = await _postService.GetPostByIdAsync(id);
                return Ok(post);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting post with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPost("create-post")]
        public async Task<ActionResult<PostResponse>> CreatePost(PostDto postDto)
        {
            try
            {
                var result = await _postService.CreatePostAsync(postDto);
                return CreatedAtAction(nameof(GetPost), new { id = result.Id }, result);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating post");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPut("update-post-by-id/{id}")]
        public async Task<IActionResult> UpdatePost(int id, PostDto postDto)
        {
            if (id != postDto.Id)
                return BadRequest("ID mismatch");

            try
            {
                var result = await _postService.UpdatePostAsync(id, postDto);
                if (!result) return BadRequest("Update failed");
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating post with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpDelete("delete-post-by-id/{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                var result = await _postService.DeletePostAsync(id);
                if (!result) return BadRequest("Delete failed");
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting post with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
