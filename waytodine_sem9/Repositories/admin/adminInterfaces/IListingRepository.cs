using waytodine_sem9.Models.admin;

namespace waytodine_sem9.Repositories.admin.adminInterfaces
{
    public interface IListingRepository
    {
        Task<object> GetAllRestaurant(int pageNumber, int pageSize);
        Task<object> GetAllOrders(int pageNumber, int pageSize);
        Task<object> GetAllUsers(int pageNumber, int pageSize);
        Task<object> GetAllDrivers(int pageNumber, int pageSize);
        Task<object> GetAllMenus(int pageNumber, int pageSize);
        Task<object> GetRestaurantDetailsById(int resid);

    }
}
