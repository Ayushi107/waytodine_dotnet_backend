using Microsoft.EntityFrameworkCore;
using waytodine_sem9.Data;
using waytodine_sem9.Repositories.admin.adminInterfaces;

namespace waytodine_sem9.Repositories.admin.adminClasses
{
    public class SearchingRepository:ISearchingRepository
    {
        private readonly ApplicationDbContext _context;

        public SearchingRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<(IEnumerable<object> Orders, int TotalRecords)> SearchOrdersAsync(string restaurantName, int pageNumber, int pageSize)
        {
            var query = _context.Order
         .Include(o => o.Restaurant)
         .Include(o => o.Customer)
         .Include(o => o.DeliveryPerson)
         .Where(o => o.Restaurant.Name.Contains(restaurantName))
         .Select(o => new
          {
              o.OrderId, 
              o.TotalAmount,
              o.OrderStatus, 
              o.PaymentStatus, 
              o.Restaurant, 
              CustomerName = o.Customer != null ? o.Customer.FirstName + " " + o.Customer.LastName : null, 
              //DeliveryPersonName = o.DeliveryPerson != null ? o.DeliveryPerson. : null,
              o.CustomerId, // Customer ID
              o.DeliveryPersonId, // Delivery person ID
              o.Customer 
          });


            int totalRecords = await query.CountAsync();
            var orders = await query
           .Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .ToListAsync();
            return (orders, totalRecords);
        }

        public async Task<(IEnumerable<object> Menus, int TotalRecords)> SearchMenusAsync(string restaurantName, int pageNumber, int pageSize)
        {
            var query = _context.MenuItem
         .Include(o => o.Restaurant)
         .Include(o => o.Category)
         .Where(o => o.Restaurant.Name.Contains(restaurantName))
         .Select(o => new
         {
             o.ItemId,
             o.Name,
             o.Description,
             o.Status,
             o.IsVeg,
             o.Price,
             o.Category.CategoryId,
             o.Restaurant
         });


            int totalRecords = await query.CountAsync();
            var menus = await query
           .Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .ToListAsync();
            return (menus, totalRecords);
        }

        public async Task<(IEnumerable<object> Users, int TotalRecords)> SearchUsersAsync(string userName, int pageNumber, int pageSize)
        {
            var query = _context.UserEntities
         .Where(o => o.FirstName.Contains(userName))
         .Select(o => new
         {
             o.UserId,
            o.FirstName,
             o.LastName,
             o.Location,
             o.Email,
             o.PhoneNumber,
         });


            int totalRecords = await query.CountAsync();
            var users = await query
           .Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .ToListAsync();
            return (users, totalRecords);
        }


        public async Task<(IEnumerable<object> Restaurants, int TotalRecords)> SearchRestaurantsAsync(string restaurantName, int pageNumber, int pageSize)
        {
            var query = _context.restaurants
         .Where(o => o.Name.Contains(restaurantName))
         .Select(o => new
         {
             o.RestaurantId,
             o.Name,
             o.Email,
             o.Location,
             o.RestaurantDocument,
            o.Country
         });


            int totalRecords = await query.CountAsync();
            var restaurants = await query
           .Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .ToListAsync();
            return (restaurants, totalRecords);
        }


    }
}
