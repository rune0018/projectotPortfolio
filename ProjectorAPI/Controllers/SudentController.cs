using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectorAPI.Data;
using ProjectorAPI.models;
using System.Collections.Generic;
using System.Linq;

namespace ProjectorAPI.Controllers
{
    [Route("api/Student")]
    [ApiController]
    public class SudentController : ControllerBase
    {
        private readonly ProjectorContext _context;
        public SudentController(ProjectorContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetStudents()
        {
            List<Student> students = _context.students.ToList();
            return Ok(students);
        }

        [HttpPost]
        public IActionResult Add(Student student)
        {
            _context.students.Add(student);
            _context.SaveChanges();
            return Created("",student);
        }
        [HttpDelete]
        [Route("{id:int:min(0)}")]
        public IActionResult Delete(int id)
        {
            Student toDelete = _context.students.Find(id);
            _context.students.Remove(toDelete);
            _context.SaveChanges();
            return Ok();
        }
        [HttpGet]
        [Route("{id:int:min(0)}")]
        public IActionResult GetDetails(int id)
        {
            var student = _context.enrollments.Include(o=>o.Student).Where(o=>o.StudentID == id);
            return Ok(student);
        }
    }
}
