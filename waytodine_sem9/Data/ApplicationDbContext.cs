using Microsoft.EntityFrameworkCore;
using waytodine_sem9.Models.admin;

namespace waytodine_sem9.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Restaurant> restaurants { get; set; }
        public DbSet<User> UserEntities { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<MenuItem> MenuItem { get; set; }
        public DbSet<DeliveryPerson> DeliveryPerson{ get; set; }
        public DbSet<RestaurantDetails> RestaurantDetails { get; set; }
        public DbSet<Feedback> Feedback { get; set; }


    }
}
