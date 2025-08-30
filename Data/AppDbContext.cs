using Microsoft.EntityFrameworkCore;
using tech_tracker_server.Models;


namespace tech_tracker_server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companyes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
    }
}
