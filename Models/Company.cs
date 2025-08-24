namespace tech_tracker_server.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User Owner { get; set; }
        public List<User> Employees { get; set; } = new List<User>();   
    }
}
