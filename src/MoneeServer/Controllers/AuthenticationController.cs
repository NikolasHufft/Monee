using BaseLibrary.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace MoneeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController(IUserAccount userAccount) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> CreateAsync(Register user)
        {
            if (user == null)
            {
                return BadRequest("Model is empty");
            }
            var result = await userAccount.CreateAsync(user);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> SignInAsync(Login user)
        {
            if (user == null)
            {
                return BadRequest("Model is empty");
            }
            var result = await userAccount.SignInAsync(user);
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync(RefreshToken token)
        {
            if (token == null)
            {
                return BadRequest("Model is empty");
            }
            var result = await userAccount.RefreshTokenAsync(token);
            return Ok(result);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsersAsync()
        {
            var users = await userAccount.GetUsers();
            if (users is null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser(ManageUser user)
        {
            // Find the user and validate the ID
            //var users = await userAccount.();
            //if (users is null)
            //{
            //    return NotFound();
            //}

            var result = await userAccount.UpdateUser(user);
            return Ok(result);
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await userAccount.GetRoles();
            if (roles is null)
            {
                return NotFound();
            }
            return Ok(roles);
        }

        [HttpDelete("delete-user/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            // Find the user and validate the ID
            //var users = await userAccount.();
            //if (users is null)
            //{
            //    return NotFound();
            //}
            var result = await userAccount.DeleteUser(id);
            return Ok(result);
        }
    }
}
