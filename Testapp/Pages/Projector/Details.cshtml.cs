using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Testapp.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace Testapp.Pages
{
    public class DetailsModel : PageModel
    {
        public Project Project;
        public async Task<IActionResult> OnGetAsync(int id)
        {
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.GetAsync($"https://localhost:44305/api/Projector/{id}");
                if (response.IsSuccessStatusCode)
                {
                    Project[] project = await response.Content.ReadFromJsonAsync<Project[]>();

                    //Debugging intesifies
                    //foreach (var o in await response.Content.ReadAsStringAsync())
                    //{
                    //    Console.Write(o);
                    //}
                    Project = project[0];
                    return Page();
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
            return StatusCode(500);
        }
    }
}
