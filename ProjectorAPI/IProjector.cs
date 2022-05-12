using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectorAPI
{
    interface IProjector
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string GithubLink { get; set; }
    }
}
