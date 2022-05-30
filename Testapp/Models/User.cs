﻿using System;

namespace Testapp.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } 
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime Logindate { get; set; }
        public User()
        {
            Id = Guid.NewGuid();
        }
    }
}
