using waytodine_sem9.Models.admin;

namespace waytodine_sem9.Repositories.admin.adminInterfaces
{
    public interface IAdminRepository
    {
        Task<Admin> GetByUsernameAndPasswordAsync(string username, string password);
        Task<Admin> AddAdmin(Admin admin);
        Task<Admin> UpdateAdmin(Admin admin);
        Task<Admin> GetAdminByEmail(string email);
        Task<Admin> GetAdminByUsername(string username);
        Task<bool> UpdatePassword(string username, string password);
       
    }
}
