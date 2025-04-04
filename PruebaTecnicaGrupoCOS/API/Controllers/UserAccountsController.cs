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
using PruebaTecnicaGrupoCOS.Helper;
using PruebaTecnicaGrupoCOS.Infrastructure.Data;

namespace PruebaTecnicaGrupoCOS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountsController : ControllerBase
    {
        private readonly IUserAccountService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<UserAccountsController> _logger;

        public UserAccountsController(IUserAccountService userService,
                                    IMapper mapper,
                                    ILogger<UserAccountsController> logger)
        {
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize]
        [HttpGet("get-user-accounts")]
        public async Task<ActionResult<IEnumerable<UserAccountResponse>>> GetUserAccounts()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all users");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-user-account-by-id/{id}")]
        public async Task<ActionResult<UserAccountResponse>> GetUserAccount(int id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting user with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("create-user-account")]
        public async Task<ActionResult<UserAccountResponse>> CreateUserAccount(UserAccountDto userDto)
        {
            try
            {
                var result = await _userService.CreateUserAsync(userDto);
                return CreatedAtAction(nameof(GetUserAccount), new { id = result.Id }, result);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPut("update-user-account-by-id/{id}")]
        public async Task<IActionResult> UpdateUserAccount(int id, UserAccountDto userDto)
        {
            if (id != userDto.Id)
                return BadRequest("ID mismatch");

            try
            {
                var result = await _userService.UpdateUserAsync(id, userDto);
                if (!result) return BadRequest("Update failed");
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating user with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpDelete("delete-user-account-by-id/{id}")]
        public async Task<IActionResult> DeleteUserAccount(int id)
        {
            try
            {
                var result = await _userService.DeleteUserAsync(id);
                if (!result) return BadRequest("Delete failed");
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting user with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
