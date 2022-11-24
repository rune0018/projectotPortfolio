namespace ProjectorAPI.models
{
    public class OfficeAssignment
    {
        public int InstructorID { get; set; }
        public string Locaton { get; set; }

        public Instructor? Instructor { get; set; }
    }
}
