

namespace CustomerService.Controllers
{
    using CustomerService.Entities;
    using CustomerService.Repositories;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpGet]
        public async Task<IEnumerable<AppUser>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        [HttpGet("{UserId}")]        
        public async Task<ActionResult<AppUser>> GetUser(int UserId)
        {
            try
            {
                var user = await _userRepository.GetUserById(UserId);
                if(user == null)
                {
                    return NotFound();
                }

                return user;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the Users database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AppUser>> PostUser([FromBody]AppUser appUser)
        {
            if (appUser == null)
                return BadRequest();

            var createduser = await _userRepository.AddUser(appUser);

            return CreatedAtAction("GetUser", new { UserId = appUser.UserId }, appUser);
        }

        [HttpDelete("{UserId}")]
        public async Task<IActionResult> DeleteProduct(int UserId)
        {
            var userToDelete = await _userRepository.GetUserById(UserId);

            if (userToDelete == null)
            {
                return NotFound($"User with Id = {UserId} not found");
            }

            _userRepository.DeleteUser(UserId);
            
            return NoContent();
        }

        [HttpGet]
        [Route("GetUserByName/{name}")]
        public async Task<ActionResult<AppUser>> GetUserByName(string name)
        {
            try
            {
                var User = await _userRepository.GetUserByName(name);

                if (User == null)
                {
                    return NotFound();
                }

                return User;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving user data from the database");
            }

        }
    }
}
