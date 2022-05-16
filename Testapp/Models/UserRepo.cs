using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Testapp.Models
{
    public class UserRepo
    {
        private List<User> Repo = new List<User>() {
            new User(){ Username = "admin",Password = "localAdmin",Role = "admin"},
            new User(){ Username = "rune001",Password = "lol",Role = "security"}
        };
//        713423 admin
//        51319244 rune001
        public User GetUser(string username,string password)
        {

            HttpClient client = new HttpClient();
            var user = new Models.User();
            user = GetUserAPI(username, password).Result;
            return user;
        }
        /// <summary>
        /// Api Call to get user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private async Task<User> GetUserAPI(string username,string password)
        {
            HttpClient client = new HttpClient();
            User[] user = new User[1];
            try
            {
                HttpResponseMessage response = await client.GetAsync($"https://localhost:44305/api/Users?Username={username}&Password={password}");
                if (response.IsSuccessStatusCode)
                {
                    user = await response.Content.ReadFromJsonAsync<User[]>();
                }
            }
            catch (Exception)
            {
                user[0] = Repo.SingleOrDefault(u => u.Username == username && u.Password == password);
            }
            return user[0];
        }
    }
}
