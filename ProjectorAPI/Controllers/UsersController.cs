using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectorAPI.models;
using System.Collections.Generic;
using System.Linq;

namespace ProjectorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private List<User> Users = new List<User>() {
            new User(){ Username = "admin",Password = "localAdmin",Role = "admin"},
            new User(){ Username = "rune001",Password = "lol",Role = "security"}
        };
        [HttpGet]
        public IActionResult Get([FromQuery]LoginModel login)
        {
            return Ok(Users.Where(user => user.Username == login.Username && user.Password == login.Password));
        }

    }
}
