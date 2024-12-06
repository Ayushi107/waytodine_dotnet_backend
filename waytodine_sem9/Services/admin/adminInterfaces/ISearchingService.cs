namespace waytodine_sem9.Services.admin.adminInterfaces
{
    public interface ISearchingService
    {
        Task<object> SearchOrdersAsync(string restaurantName, int pageNumber, int pageSize);
        Task<object> SearchMenusAsync(string restaurantName, int pageNumber, int pageSize);
        Task<object> SearchUsersAsync(string userName, int pageNumber, int pageSize);
        Task<object> SearchREstaurantsAsync(string restaurantName, int pageNumber, int pageSize);


    }
}
