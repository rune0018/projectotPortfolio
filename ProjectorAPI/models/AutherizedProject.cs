﻿namespace ProjectorAPI.models
{
    public class AutherizedProject
    {
        public Project project { get; set; }
        public string username { get; set; }
        public string role { get; set; }
    }
}