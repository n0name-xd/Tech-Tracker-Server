namespace tech_tracker_server.Models
{
    public class User
    {
        private Roles _role;
        private string _userphone;

        public User(string userName, Roles role, string userPhone)
        {
            UserName = userName;
            _role = role;
            _userphone = userPhone;
        }

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserName { get; set; }
        public string? UserPassword { get; set; } = string.Empty;
        public string UserPhone 
        {
            get => _userphone;
            private set => _userphone = value;
        }
        public string? UserEmail { get; set; } = string.Empty;
        public Roles Role 
        { 
            get => _role;
            private set => _role = value; 
        }
    }
}
