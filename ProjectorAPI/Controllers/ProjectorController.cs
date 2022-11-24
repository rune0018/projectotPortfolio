using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectorAPI.Data;
using ProjectorAPI.models;
using Microsoft.AspNetCore.Authorization;

namespace ProjectorAPI.Controllers
{
    [Route("api/Projector")]
    [ApiController]
    [AllowAnonymous]
    public class ProjectorController : ControllerBase
    {
        private static List<Project> data = new();
        //Magic Linking with ../data/ProjectorContext.cs 2
        private readonly ProjectorContext _context;
        public ProjectorController(ProjectorContext context)
        {
            _context = context;
        }
        //[HttpPost]
        //public IActionResult Login(LoginModel model)
        //{
        //    Console.WriteLine(model.Username);
        //    return Ok();
        //}

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                if (!_context.projects.Any())
                {
                    return NotFound();
                }
                return Ok(_context.projects);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database faliure {e.Message}");
            }

        }
        [Authorize]
        [HttpPost]
        public IActionResult Post(AutherizedProject aproject)
        {
            if(Exists(aproject.username,aproject.id))
            try
            {
                _context.projects.Add(aproject.project);
                _context.SaveChanges();
                return Created($"/api/projector/{aproject.project.ID}", aproject.project);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database faliure {e.Message}");
            }
            return BadRequest("no user found");
        }
        [Route("{id:int:min(0)}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            if (!_context.projects.Any())
            {
                return NotFound();
            }
            return Ok(_context.projects.Where(x=>x.ID == id));
        }
        [Authorize]
        [Route("{id:int:min(0)}")]
        [HttpDelete]
        public IActionResult Delete(int id,string username,string guid)
        {
            if (!Exists(username, guid)) 
                return Unauthorized();
            Project toRemove = _context.projects.Where(x => x.ID == id).First();
            _context.projects.Remove(toRemove);
            _context.SaveChanges();
            return Ok();
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
    }
}
