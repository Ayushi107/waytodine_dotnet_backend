using waytodine_sem9.Models.admin;

namespace waytodine_sem9.Repositories.admin.adminInterfaces
{
    public interface IAdminRepository
    {
        Task<Admin> GetByUsernameAndPasswordAsync(string username, string password);
        Task<Admin> AddAdmin(Admin admin);
        Task<Admin> UpdateAdmin(Admin admin);
        Task<Admin> GetAdmin();
        Task<Admin> GetAdminByEmail(string email);
        Task<Admin> GetAdminByUsername(string username);
        Task<bool> UpdatePassword(string username, string password);
        Task<bool> GetRestaurantById(int id);
        Task<bool> GetDeliveryPersonById(int id);
        Task<User> GetUserByEmail(string email);
        Task<string> VerifyRestaurant(int id);
        Task<string> VerifyDriver(int id);


    }
}
