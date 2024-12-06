using waytodine_sem9.Models.admin;
using waytodine_sem9.Repositories.admin.adminInterfaces;
using waytodine_sem9.Services.admin.adminInterfaces;

namespace waytodine_sem9.Services.admin.adminClasses
{
    public class ListingService:IListingService
    {
        private readonly IListingRepository _listingRepository;

        public ListingService(IListingRepository listingRepository)
        {
            _listingRepository = listingRepository;
        }

        public async Task<object> GetAllRestaurantAsync(int pageNumber, int pageSize)
        {
            return await _listingRepository.GetAllRestaurant(pageNumber, pageSize);
        }

        public async Task<object> GetAllOrdersAsync(int pageNumber, int pageSize)
        {
            return await _listingRepository.GetAllOrders(pageNumber, pageSize);
        }

        public async Task<object> GetAllUsersAsync(int pageNumber, int pageSize)
        {
            return await _listingRepository.GetAllUsers(pageNumber, pageSize);
        }

        public async Task<object> GetAllDriversAsync(int pageNumber, int pageSize)
        {
            return await _listingRepository.GetAllDrivers(pageNumber, pageSize);
        }
        public async Task<object> GetAllMenusAsync(int pageNumber, int pageSize)
        {
            return await _listingRepository.GetAllMenus(pageNumber, pageSize);
        }
    }
}
