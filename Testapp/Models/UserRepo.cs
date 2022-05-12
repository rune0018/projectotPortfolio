using System;
using System.Collections.Generic;
using System.Linq;
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
            var user = Repo.SingleOrDefault(u => u.Username == username && u.Password == password);
            return user;
        }
    }
}
