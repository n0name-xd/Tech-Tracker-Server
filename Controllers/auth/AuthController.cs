using Microsoft.AspNetCore.Mvc;
using tech_tracker_server.Controllers.auth.DTO;

namespace tech_tracker_server.Controllers.auth
{
    [Route("api/auth/user")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public void Post([FromBody] UserCredentials credentials)
        {
            Console.WriteLine(credentials.UserName);
            Console.WriteLine(credentials.Password);
        }
    }
}
