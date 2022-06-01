using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectorAPI.models;
using System.Collections.Generic;
using System.Linq;
using System;
using ProjectorAPI.Data;

namespace ProjectorAPI.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static List<User> MyUsers = new List<User>() {
            new User(){ Username = "admin",Password = Encrypt.sha512("Admin"),Role = "admin"},
            new User(){ Username = "rune001",Password = "lol",Role = "security"}
        };

        private readonly ProjectorContext _context;
        public UsersController(ProjectorContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get([FromQuery] LoginModel login)
        {
            if (!_context.users.Any())
            {
                _context.users.Add(new models.User { Username = "admin", Password = Encrypt.sha512("localAdmin"), Role = "admin" });
                _context.SaveChanges();
            }
            try
            {
                var user = _context.users.Single(user => user.Username == login.Username && user.Password == Encrypt.sha512(login.Password));
                user.Logindate = DateTime.Now;
                _context.users.Update(user);
                _context.SaveChanges();
                return Ok(user);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [NonAction]
        public bool Exists(string username, string id)
        {
            if (_context.users.Where(u => u.Username == username &&
            u.Id.ToString() == id &&
            DateTime.Compare(DateTime.Now, u.Logindate.AddHours(1)) <= 0)
                .Count() >= 1)
            {
                return true;
            }
            return false;
        }
        
        [HttpPost]
        public IActionResult Post(RegisterForm registerForm)
        {
            if(Exists("admin", registerForm.id))
            {
                User toAdd = new models.User { Username = registerForm.username, Password = Encrypt.sha512(registerForm.password) };
                _context.users.Add(toAdd);
                _context.SaveChanges();
                return Created($"/api/Users/{toAdd.Id}",toAdd);
            }
            return Unauthorized();
        }

    }
}
