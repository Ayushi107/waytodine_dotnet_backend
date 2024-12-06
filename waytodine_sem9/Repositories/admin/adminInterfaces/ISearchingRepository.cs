namespace waytodine_sem9.Repositories.admin.adminInterfaces
{
    public interface ISearchingRepository
    {
        Task<(IEnumerable<object> Orders, int TotalRecords)> SearchOrdersAsync(string restaurantName, int pageNumber, int pageSize);

        Task<(IEnumerable<object> Menus, int TotalRecords)> SearchMenusAsync(string restaurantName, int pageNumber, int pageSize);

        Task<(IEnumerable<object> Users, int TotalRecords)> SearchUsersAsync(string userName, int pageNumber, int pageSize);

        Task<(IEnumerable<object> Restaurants, int TotalRecords)> SearchRestaurantsAsync(string restaurantName, int pageNumber, int pageSize);


    }
}
