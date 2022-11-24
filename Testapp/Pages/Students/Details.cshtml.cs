using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Testapp.Models;

namespace Testapp.Pages.Students
{
    public class DetailsModel : PageModel
    {

        public int Id { get; set; }

        public Enrollment? Student { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"https://localhost:44305/api/Student/{id}");
            response.EnsureSuccessStatusCode();
            Student = await response.Content.ReadFromJsonAsync<Enrollment>();
            return Page();
        }
    }
}
