namespace tech_tracker_server.Models
{
    public class Company
    {
        public Company() { }
        public Company (string name, User owner )
        {
            Name = name;
            Owner = owner;
        }

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public User Owner { get; set; }
        public List<User> Employees { get; set; } = new List<User>();
        public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
