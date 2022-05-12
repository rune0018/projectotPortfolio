namespace ProjectorAPI.models
{
    public class User
    {
        private static int _INC { get; set; }
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public User()
        {
            Id = _INC++.ToString();
        }
    }
}
