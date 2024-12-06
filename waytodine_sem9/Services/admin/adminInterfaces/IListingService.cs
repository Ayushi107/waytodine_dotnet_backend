using waytodine_sem9.Models.admin;

namespace waytodine_sem9.Services.admin.adminInterfaces
{
    public interface IListingService
    {
        Task<object> GetAllRestaurantAsync(int pageNumber, int pageSize);
        Task<object> GetAllOrdersAsync(int pageNumber, int pageSize);
        Task<object> GetAllUsersAsync(int pageNumber, int pageSize);
        Task<object> GetAllDriversAsync(int pageNumber, int pageSize);
        Task<object> GetAllMenusAsync(int pageNumber, int pageSize);
      
    }
}
