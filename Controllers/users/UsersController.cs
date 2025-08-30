using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using tech_tracker_server.Controllers.users.DTO;
using tech_tracker_server.Models;
using tech_tracker_server.Data;

namespace tech_tracker_server.Controllers.users
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Getting a list of users", Description = "")]
        public ActionResult<List<User>> Get()
        {
            var users = _context.Users.ToList(); 
            return Ok(users);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Getting a user by ID", Description = "Returns the user by their unique ID")]
        [SwaggerResponse(200, "The user has been found", typeof(User))]
        [SwaggerResponse(404, "The user was not found")]
        public ActionResult<User> Get(string id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound($"User with id {id} not found");
            }

            return Ok(user);
        }

        [HttpPost]
        [
            SwaggerOperation(Summary = "Create a new user", 
            Description = "Accepts user data and creates it in the system. Accepts roles as enum 0 = Owner, 1 = Manager, 2 = Employee")
        ]
        [SwaggerResponse(200, "The user was successfully created", typeof(User))]
        [SwaggerResponse(400, "Incorrect data")]
        public ActionResult<User> Post([FromBody] CreateUserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest("Incorrect data was transmitted");
            }

            var newUser = new User(userDto.UserName, userDto.Role, userDto.UserPhone);

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return Ok(newUser);
        }


        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Updating a user by ID", Description = "Updates the data of an existing user")]
        [SwaggerResponse(200, "The user has been successfully updated", typeof(User))]
        [SwaggerResponse(404, "The user was not found")]
        public ActionResult<User> Put(string id, [FromBody] UpdateUserDto updateUserDto)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound($"User with id {id} not found");
            }

            user.UserName = updateUserDto.UserName ?? user.UserName;

            _context.SaveChanges();

            return Ok(user);
        }


        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deleting a user by ID", Description = "Deletes a user by their unique ID")]
        [SwaggerResponse(200, "The user has been successfully deleted")]
        [SwaggerResponse(404, "The user was not found")]
        public IActionResult Delete(string id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound($"User with id: ${id} not found");
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return Ok("The user has been successfully deleted");
        }
    }
}
