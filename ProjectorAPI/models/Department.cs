using System;

namespace ProjectorAPI.models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Budget { get; set; }
        public DateTime StartDate { get; set; }

        public Instructor Instructor { get; set; }
    }
}
