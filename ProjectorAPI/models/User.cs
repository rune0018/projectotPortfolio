using System;

namespace ProjectorAPI.models
{
    public class User
    {
        private static int _INC { get; set; }
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public User()
        {
            Id = Guid.NewGuid();
        }
    }
}
