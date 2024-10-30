using Microsoft.EntityFrameworkCore;
using waytodine_sem9.Models.admin;

namespace waytodine_sem9.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Admin> Admins { get; set; }
    }
}
