namespace Testapp.Models
{
    public enum Grade
    {
        A,B,C,D,E,F
    }

    public class Enrollment
    {
        public int ID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Grade Grade { get; set; }


        public object? Course { get; set; }
        public Student? Student { get; set; }
    }
}
