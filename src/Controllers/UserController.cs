using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.Entity;
using src.Services.User;
using src.Utils;
using static src.DTO.UserDTO;

namespace src.Controllers
{
    [Route("/api/v1/[controller]")]
    [ApiController] //User
    public class UserController : ControllerBase
    {
        /// <summary>
        /// make the list static so all the sellers and the customers access it.
        /// make lis info.
        /// get, post, put and delete method for user without consider is(seller, customer).
        /// </summary>


        protected readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// http://localhost:5125/api/v1/User/SignUp
        /// <API>
        /// {
        ///     "name": "",
        ///     "phoneNumber": "+1-9",
        ///     "email": "@ .com",
        ///     "password": "Aa-Zz 1-8"
        ///  }
        /// </API>
        /// return user info
        ///
        /// </summary>
        [HttpPost("SignUp")] //SignUp
        public async Task<ActionResult<UserCreateDto>> CreateOne([FromBody] UserCreateDto createDto)
        {
            var userCretaed = await _userService.CreateOneAsync(createDto);
            if (userCretaed == null)
            {
                return Conflict("The email is already in use.");
            }
            return Ok(userCretaed);
        }

        /// <summary>
        /// http://localhost:5125/api/v1/User/LogIn
        /// <API>
        ///{
        ///     "email": "",
        ///     "password": ""
        /// }
        /// </API>
        /// return Token
        ///</summary>

        [HttpPost("LogIn")] //Login
        public async Task<ActionResult<string>> LogInOne([FromBody] UserLoginDto createDto)
        {
            var token = await _userService.LogInAsync(createDto);
            return Ok(token);
        }

        /// <summary>
        /// get all user info just by Admin
        /// in postman => authorization => choose the AuthType "Bearer Token" => then add the token
        /// the token is the ine give to you when you login
        /// </summary>

        [HttpGet]
        [Authorize] // --> For All users
                    // [Authorize(Roles ="Admin")] //--> For admins
        public async Task<ActionResult<List<UserReadDto>>> GetAllAsync()
        {
            var users = await _userService.GetAllAsync();

            if (users == null || !users.Any())
            {
                return NotFound("No users found.");
            }
            return Ok(users);
        }

        /// http://localhost:5125/api/v1/User/Profile
        /// <summary>
        /// in postman => authorization => choose the AuthType "Bearer Token" => then add the token
        /// the token is the ine give to you when you login
        /// </summary>
        /// Profile just by user owner
        [HttpGet("Profile")] // Endpoint to view user profile
        [Authorize]
        public async Task<ActionResult<UserProfileDto>> GetProfileIdAsync()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = await _userService.GetProfileIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

        /// in postman => authorization => choose the AuthType "Bearer Token" => then add the token
        /// the token is the ine give to you when you login
        /// /// <API>
        /// {
        ///  "password": "1457337"
        ///  }
        ///  </API>
        ///  return true
        /// </summary>
        [HttpPut("UpdateProfile")] // Endpoint to view user profile
        [Authorize]
        public async Task<ActionResult<UserProfileDto>> UpdateProfileAsync(UserProfileDto updateDto)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var updatedUserDto = await _userService.UpdateOneAsync(userId, updateDto);

            if (updatedUserDto == null)
            {
                return NotFound("User not found.");
            }

            return Ok(updatedUserDto);
        }

        /// <summary>
        /// in postman => authorization => choose the AuthType "Bearer Token" => then add the token
        /// the token is the ine give to you when you login
        /// <API>
        /// {
        ///  "password": "1457337"
        ///  }
        ///  </API>
        ///  return true
        /// </summary>

        [HttpPut("UpdatePassword")]
        [Authorize]
        public async Task<ActionResult<bool>> UpdatePassword(PasswordUpdateDto updateDto)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var isUpdated = await _userService.UpdatePasswordAsync(userId, updateDto);

            if (!isUpdated)
            {
                return NotFound("User not found.");
            }

            return Ok(isUpdated);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            try
            {
                var result = await _userService.DeleteOneAsync(userId);
                if (result)
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
