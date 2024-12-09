using Microsoft.EntityFrameworkCore;
using waytodine_sem9.Data;
using waytodine_sem9.Models.admin;
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
         .Include(o=>o.CartItems)
         .Where(o => o.Restaurant.Name.ToLower().Contains(restaurantName.ToLower()))
         .Select(o => new
          {
              o.OrderId, 
              o.TotalAmount,
              o.OrderStatus, 
              o.PaymentStatus,
             Restaurant = new
             {
                 o.Restaurant.RestaurantId,
                 o.Restaurant.Name,
                 o.Restaurant.Email,
                 o.Restaurant.PhoneNumber,
                 o.Restaurant.Location,
                 o.Restaurant.City,
                 o.Restaurant.Country,
                 o.Restaurant.Status,
                 o.Restaurant.CreatedAt,
                 o.Restaurant.UpdatedAt
             },
             CartItems = _context.CartItems
                .Where(c => c.OrderId == o.OrderId)
                .Select(c => new
                {
                    c.CartId,
                    c.CustomerId,
                    c.Quantity,
                    c.Status,
                    c.Total,
                    c.ItemId,
                    c.RestaurantId,
                    c.CreatedAt,
                    c.UpdatedAt,
                    Item = new
                    {
                        c.Items.ItemId,
                        c.Items.Name,
                        c.Items.Price,
                        c.Items.Description,
                        c.Items.ItemImage
                    }
                }).ToList(),
             Customer = new
             {
                 o.Customer.UserId,
                 o.Customer.FirstName,
                 o.Customer.LastName,
                 o.Customer.Email,
                 o.Customer.PhoneNumber,
                 o.Customer.Status,
                 o.Customer.Location,
                 o.Customer.ProfilePic,
                 o.Customer.CreatedAt,
                 o.Customer.UpdatedAt
             },
             //DeliveryPersonName = o.DeliveryPerson != null ? o.DeliveryPerson. : null,
             o.CustomerId, // Customer ID
              o.DeliveryPersonId, // Delivery person ID
             
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
                .Include(o => o.Restaurant)  // Include Restaurant data
                .Include(o => o.Category)    // Include Category data
                .Where(o => o.Name.ToLower().Contains(restaurantName.ToLower())) // Search filter for restaurantName
                .Select(o => new
                {
                    o.ItemId,
                    o.Name,
                    o.Description,
                    o.Price,
                    o.ItemImage,
                    o.Status,
                    o.IsVeg,
                    // Include Category details
                    Category = new
                    {
                        o.Category.CategoryId,
                        o.Category.CategoryName,
                    },
                    // Include Restaurant details
                    Restaurant = new
                    {
                        o.Restaurant.RestaurantId,
                        o.Restaurant.Name,
                        o.Restaurant.Location
                    }
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
         .Where(o => o.FirstName.ToLower().Contains(userName.ToLower()))
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
         .Where(o => o.Name.ToLower().Contains(restaurantName.ToLower()))
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
