using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using tech_tracker_server.Controllers.users.DTO;
using tech_tracker_server.Models;

namespace tech_tracker_server.Controllers.users
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        [SwaggerOperation(Summary = "Получение списка пользователей", Description = "")]
        public List<User> Get()
        {
            return new List<User>();
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        [
            SwaggerOperation(Summary = "Создать нового пользователя", 
            Description = "Принимает данные пользователя и создает его в системе. Принимает роли как enum 0 = Owner, 1 = Manager, 2 = Employee")
        ]
        [SwaggerResponse(200, "Пользователь успешно создан", typeof(User))]
        [SwaggerResponse(400, "Некорректные данные")]
        public User Post([FromBody] CreateUserDto userDto)
        {
            if (userDto == null)
            {
                throw new ArgumentNullException(nameof(userDto));
            }

            var newUser = new User(userDto.UserName, userDto.Role, userDto.UserPhone);

            return newUser;
        }


        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
