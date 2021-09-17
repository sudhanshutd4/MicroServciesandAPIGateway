

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
        [Route("GetUsers")]
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
        [Route("AddUser")]
        public async Task<ActionResult<AppUser>> AddUser([FromBody]AppUser appUser)
        {
            if (appUser == null)
                return BadRequest();

            var createduser = await _userRepository.AddUser(appUser);

            return CreatedAtAction("GetUser", new { UserId = appUser.UserId }, appUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var userToDelete = await _userRepository.GetUserById(id);

            if (userToDelete == null)
            {
                return NotFound($"User with Id = {id} not found");
            }

            _userRepository.DeleteUser(id);
            
            return NoContent();
        }

        [HttpGet("{name}")]
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
