using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using waytodine_sem9.Models.restaurant;

namespace waytodine_sem9.MansiData
{
    public class ApplicationDbContextMansi : DbContext
    {
        public ApplicationDbContextMansi(DbContextOptions<ApplicationDbContextMansi> options) : base(options) { }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<DeliveryPerson> DeliveryPersons { get; set; }
        public DbSet<MenuItem> MenuItem { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<RestaurantDetails> RestaurantDetails { get; set; }
    }
}
