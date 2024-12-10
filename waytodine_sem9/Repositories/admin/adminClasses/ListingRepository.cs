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
                .Include(o => o.Customer)
                .Include(o => o.CartItems)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new
                {
                    o.OrderId,
                    o.CustomerId,
                    o.RestaurantId,
                    o.DeliveryPersonId,
                    o.TotalAmount,
                    o.Discount,
                    o.OrderStatus,
                    o.PaymentStatus,
                    o.IsAccept,
                    o.CreatedAt,
                    o.UpdatedAt,
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
                }).ToList()
                })
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


        public async Task<object> GetRestaurantDetailsById(int resid)
        {
            var resataurantdetail = await _context.RestaurantDetails
               .Where(rd => rd.RestaurantId == resid)
               .Select(rd => new {
                   rd.RestaurantId,
                   rd.RestaurantDetailsId,
                   rd.OpeningHoursWeekdays,
                   rd.OpeningHoursWeekends,
                   rd.CurrentOfferDiscountRate,
                   rd.Specialities
               }).ToListAsync();

            return resataurantdetail;
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
                .Include(m=>m.Category)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(m => new
                {
                    m.ItemId,
                    m.Name,
                    m.Description,
                    m.Price,
                    m.ItemImage,
                    m.Status,
                    m.IsVeg,
                    Category = new
                    {
                        m.Category.CategoryId,
                        m.Category.CategoryName,
                    }
                })
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
