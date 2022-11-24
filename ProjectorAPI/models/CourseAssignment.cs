﻿namespace ProjectorAPI.models
{
    public class CourseAssignment
    {
        public int InstructorID { get; set; }
        public Instructor? Instructor { get; set; }
        public int CourseID { get; set; }
        public Course? Course { get; set; }
    }
}
