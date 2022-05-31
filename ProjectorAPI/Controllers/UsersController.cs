using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectorAPI.models;
using System.Collections.Generic;
using System.Linq;
using System;
using ProjectorAPI.Data;

namespace ProjectorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static List<User> MyUsers = new List<User>() {
            new User(){ Username = "admin",Password = "localAdmin",Role = "admin"},
            new User(){ Username = "rune001",Password = "lol",Role = "security"}
        };

        private readonly ProjectorContext _context;
        public UsersController(ProjectorContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get([FromQuery]LoginModel login)
        {
            var users = MyUsers.Where(user => user.Username == login.Username && user.Password == login.Password);
            int whatToUpdate = MyUsers.IndexOf(users.First());
            MyUsers[whatToUpdate].Logindate=DateTime.Now;
            return Ok(MyUsers.Where(user => user.Username == login.Username && user.Password == login.Password));
        }

        public static bool Exists(string username, string id)
        {
            if(MyUsers.Where(u=> u.Username == username &&
            u.Id.ToString() == id &&
            DateTime.Compare(DateTime.Now, u.Logindate.AddHours(1)) <=0)
                .Count() >=1)
            {
                return true;
            }
            return false;
        }

    }
}
