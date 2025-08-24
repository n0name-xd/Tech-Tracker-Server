using tech_tracker_server.Models;

namespace tech_tracker_server.Controllers.users.DTO
{
    public class CreateUserDto
    {
        public required string UserName { get; set; }

        public required Roles Role { get; set; }

        public required string UserPhone { get; set; }
    }
}
