using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjectorAPI.models
{
    
    public class Project : IProjector
    {
        
        //public static List<Project> Projects = new List<Project>();
        
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string GithubLink { get; set; }
        /// <summary>
        /// Defines a new project using the constructor
        /// </summary>
        /// <param name="title">title of the project</param>
        /// <param name="description">description of the project</param>
        /// <param name="githubLink">the github link of the project</param>
        public Project(string title,string description, string githubLink = "https://github.com/")
        {
            Title = title;
            Description = description;
            GithubLink = githubLink;
            //Projects.Add(this);
        }
    }
}
