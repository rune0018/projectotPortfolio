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
            return BadRequest();

        }
        [Authorize]
        [HttpPost]
        public IActionResult Post(Project project)
        {
            try
            {
                _context.projects.Add(project);
                _context.SaveChanges();
                return Created($"/api/projector/{project.ID}", project);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database faliure {e.Message}");
            }
            return BadRequest();
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
        public IActionResult Delete(int id)
        {
            Project toRemove = _context.projects.Where(x => x.ID == id).First();
            _context.projects.Remove(toRemove);
            _context.SaveChanges();
            return Ok();
        }
    }
}
