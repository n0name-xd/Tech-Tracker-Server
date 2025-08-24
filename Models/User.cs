namespace tech_tracker_server.Models
{
    public class User
    {
        private string _userName;
        private Roles _role;
        private string _userphone;

        public User(string userName, Roles role, string userPhone)
        {
            _userName = userName;
            _role = role;
            _userphone = userPhone;
        }

        public int? Id { get; set; }
        public string UserName 
        { 
            get => _userName;
            private set => _userName = value;
        }
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
