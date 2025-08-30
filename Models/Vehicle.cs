namespace tech_tracker_server.Models
{
    public class Vehicle
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public string Description { get; set; }

        public string VehicleType { get; set; }
    }
}
