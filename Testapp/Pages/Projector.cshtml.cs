using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;
using Testapp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Testapp.Pages
{
    [AllowAnonymous]
    public class Projecter : PageModel
    {
        public List<Project> Projects = new();

        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public string Description { get; set; }
        [BindProperty]
        public string GithubLink { get; set; }
        
        
        public async Task<IActionResult> OnGetAsync()
        {
            HttpClient client = new HttpClient();
            try
            {
                
                HttpResponseMessage response = await client.GetAsync("https://localhost:44305/api/Projector");
                
                if (response.IsSuccessStatusCode)
                {
                    Project[] project = await response.Content.ReadFromJsonAsync<Project[]>();

                    //Debugging intesifies
                    //foreach (var o in await response.Content.ReadAsStringAsync())
                    //{
                    //    Console.Write(o);
                    //}
                    Projects.AddRange(project);
                    return Page();
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
            return StatusCode(500);
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync($"https://localhost:44305/api/Projector/{id}");
            return Page();
        }
        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }
            HttpClient client = new HttpClient();
            if (Projects.Count() < 1)
            {
                Projects = Project.Projects;
            }
            if (Title != null)
            {
                Project p = new Project(Title, Description, GithubLink);
                AutherizedProject autherizedProject = new AutherizedProject
                {
                    project = p,
                    username = User.Identity.Name,
                    role = User.FindFirstValue(ClaimTypes.Role)
                };
                HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:44305/api/Projector", autherizedProject);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(response.Headers.Location.ToString());
            }
            return Page();
        }
    }
}
