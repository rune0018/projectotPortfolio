using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Testapp.Models;

namespace Testapp.Pages
{
    public class StudentsModel : PageModel
    {
        public List<Student> Students { get; set; } = new List<Student>();

        public Student student { get; set; }

        public async Task<IActionResult> OnGet()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://localhost:44305/api/Student");
            response.EnsureSuccessStatusCode();
            var students = await response.Content.ReadFromJsonAsync<Student[]>();
            Students.AddRange(students);
            return Page();
         }

        public async Task<IActionResult> OnPostAddAsync(Student student)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync<Student>("https://localhost:44305/api/Student",student);
            response.EnsureSuccessStatusCode();
            return Redirect("/students");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync($"https://localhost:44305/api/Student/{id}");
            response.EnsureSuccessStatusCode();
            return Redirect("/students");
        }
    }
}
