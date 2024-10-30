using Microsoft.EntityFrameworkCore;
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


      

       
    }
}
