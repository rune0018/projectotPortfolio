using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectorAPI.Data
{
    public class DbInitializer
    {
        public static void Initialize(ProjectorContext context)
        {
            context.Database.EnsureCreated();

            if (context.projects.Any()&&context.users.Any())
            {
                return;
            }
            context.users.Add(new models.User { Username = "admin", Password = "localAdmin" });
            //seed data if needed
        }
    }
}
