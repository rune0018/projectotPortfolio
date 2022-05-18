namespace Testapp.Models
{
    //used to verify that a user exists
    public class AutherizedProject
    {
        public Project project { get; set; }
        public string username { get; set; }
        public string id { get; set; }
    }
}
