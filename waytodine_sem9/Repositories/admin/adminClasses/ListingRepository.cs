using Microsoft.EntityFrameworkCore;
using waytodine_sem9.Data;
using waytodine_sem9.Models.admin;
using waytodine_sem9.Repositories.admin.adminInterfaces;

namespace waytodine_sem9.Repositories.admin.adminClasses
{
    public class ListingRepository:IListingRepository
    {
        private readonly ApplicationDbContext _context;

        public ListingRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<object> GetAllRestaurant(int pageNumber, int pageSize)
        {
            var totalRecords = await _context.restaurants.CountAsync();
            var restaurants = await _context.restaurants
                .Include(r => r.RestaurantDetails)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize),
                Data = restaurants
            };
        }

        public async Task<object> GetAllOrders(int pageNumber, int pageSize)
        {
            var totalRecords = await _context.Order.CountAsync();
            var orders = await _context.Order
                .Include(o => o.Restaurant)
                .Include(o=>o.Customer)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize),
                Data = orders
            };
        }

        public async Task<object> GetAllUsers(int pageNumber, int pageSize)
        {
            var totalRecords = await _context.UserEntities.CountAsync();
            var users = await _context.UserEntities
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize),
                Data = users
            };
        }


        public async Task<object> GetAllMenus(int pageNumber, int pageSize)
        {
            var totalRecords = await _context.MenuItem.CountAsync();
            var menus = await _context.MenuItem
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize),
                Data = menus
            };
        }

        public async Task<object> GetAllDrivers(int pageNumber, int pageSize)
        {
            var totalRecords = await _context.DeliveryPerson.CountAsync();
            var drivers = await _context.DeliveryPerson
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize),
                Data = drivers
            };
        }
    }
}
