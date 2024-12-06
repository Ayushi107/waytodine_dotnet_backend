using waytodine_sem9.Repositories.admin.adminInterfaces;
using waytodine_sem9.Services.admin.adminInterfaces;

namespace waytodine_sem9.Services.admin.adminClasses
{
    public class SearchingService:ISearchingService
    {
        private readonly ISearchingRepository _searchingRepository;

        public SearchingService(ISearchingRepository searchingRepository)
        {
            _searchingRepository = searchingRepository;
        }

        public async Task<object> SearchOrdersAsync(string restaurantName, int pageNumber, int pageSize)
        {
            if (string.IsNullOrEmpty(restaurantName))
                throw new ArgumentException("Restaurant name is required.");

            var (orders, totalRecords) = await _searchingRepository.SearchOrdersAsync(restaurantName, pageNumber, pageSize);

            return new
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize),
                Data = orders
            };
        }

        public async Task<object> SearchMenusAsync(string restaurantName, int pageNumber, int pageSize)
        {
            if (string.IsNullOrEmpty(restaurantName))
                throw new ArgumentException("Restaurant name is required.");

            var (menus, totalRecords) = await _searchingRepository.SearchMenusAsync(restaurantName, pageNumber, pageSize);

            return new
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize),
                Data = menus
            };
        }

        public async Task<object> SearchUsersAsync(string restaurantName, int pageNumber, int pageSize)
        {
            if (string.IsNullOrEmpty(restaurantName))
                throw new ArgumentException("Username is required.");

            var (users, totalRecords) = await _searchingRepository.SearchUsersAsync(restaurantName, pageNumber, pageSize);

            return new
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize),
                Data = users
            };
        }

        public async Task<object> SearchREstaurantsAsync(string restaurantName, int pageNumber, int pageSize)
        {
            if (string.IsNullOrEmpty(restaurantName))
                throw new ArgumentException("Restaurant name is required.");

            var (restaurants, totalRecords) = await _searchingRepository.SearchRestaurantsAsync(restaurantName, pageNumber, pageSize);

            return new
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize),
                Data = restaurants
            };
        }
    }
}
