using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using waytodine_sem9.Data;
using waytodine_sem9.Models.admin;
using waytodine_sem9.Repositories.admin.adminInterfaces;

namespace waytodine_sem9.Repositories.admin.adminClasses
{
    public class AdminRepository:IAdminRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Admin> GetByUsernameAndPasswordAsync(string username, string password)
        {
            return await _context.Admins.FirstOrDefaultAsync(a => a.Username == username && a.Password == password);
        }

        public async Task<Admin> AddAdmin(Admin admin)
        {
            await _context.Admins.AddAsync(admin);
            await _context.SaveChangesAsync();
            return admin;
        }

        public async Task<Admin> UpdateAdmin(Admin admin)
        {
            var existingAdmin = await _context.Admins.FirstOrDefaultAsync(a => a.Id == admin.Id);
            if (existingAdmin == null)
            {
                return null; // Admin not found
            }

            // Update the necessary fields
            existingAdmin.Username = admin.Username;
            existingAdmin.Email = admin.Email;
            existingAdmin.Image = admin.Image;
            existingAdmin.Gender = admin.Gender;

            _context.Admins.Update(existingAdmin);
            await _context.SaveChangesAsync();
            return existingAdmin;
        }

        public async Task<Admin> GetAdminByEmail(string email)
        {
            return await _context.Admins.SingleOrDefaultAsync(a => a.Email == email);
        }

        public async Task<Admin> GetAdminByUsername(string username)
        {
            return await _context.Admins.SingleOrDefaultAsync(a => a.Username == username);
        }

        public async Task<bool> UpdatePassword(string username, string password)
        {
            var admin = await GetAdminByUsername(username);
            if (admin == null)
            {
                return false;
            }

            //admin.Password = hashedPassword;
            admin.Password = password;
            _context.Admins.Update(admin);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Admin> GetAdmin()
        {
            return await _context.Admins.FirstOrDefaultAsync();
        }

        public async Task<bool> GetRestaurantById(int id)
        {
            //return await _context.Restaurants.SingleOrDefaultAsync(a => a.Id == id);
            return true;
        }
        public async Task<bool> GetDeliveryPersonById(int id)
        {
            //return await _context.Restaurants.SingleOrDefaultAsync(a => a.Id == id);
            return true;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.UserEntities.SingleOrDefaultAsync(a => a.Email == email);
        }

        public async Task<string> VerifyRestaurant(int id)
        {
            // Fetch the restaurant entity
            var restaurant = await _context.restaurants
                .FirstOrDefaultAsync(r => r.RestaurantId == id);

            if (restaurant == null)
            {
                return null;
            }

            // Update the password and status
            restaurant.Status = 1;

            // Update the restaurant entity in the database
            _context.restaurants.Update(restaurant);
            await _context.SaveChangesAsync();



            return restaurant.Email; // Return the email for further processing
        }


        public async Task<string> VerifyDriver(int id)
        {
            var driver = await _context.DeliveryPerson
                  .FirstOrDefaultAsync(r => r.DeliveryPersonId == id);
            if (driver == null)
            {
                return null;
            }
            driver.Status = 1;
            _context.DeliveryPerson.Update(driver);
            await _context.SaveChangesAsync();
            string email = driver.DriverEmail;
            return email;

        }


        public async Task<Restaurant> UpdateResPassword(int resid, string password)
            {

            var res = await _context.restaurants
                  .FirstOrDefaultAsync(r => r.RestaurantId == resid);
            if (res == null)
            {
                return null;
            }
            res.Password = password;
            _context.restaurants.Update(res);
            await _context.SaveChangesAsync();
            return res;

        }

    }
}
