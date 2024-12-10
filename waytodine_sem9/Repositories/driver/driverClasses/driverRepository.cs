using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using waytodine_sem9.Data;
using waytodine_sem9.Models.admin;
using waytodine_sem9.Repositories.driver.driverInterfaces;

namespace waytodine_sem9.Repositories.driver.driverClasses
{
    public class driverRepository:IdriverRepository
    {
        private readonly ApplicationDbContext _context;

        public driverRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DeliveryPerson> AddDriver(DeliveryPerson driver)
        {
            await _context.DeliveryPerson.AddAsync(driver);
            await _context.SaveChangesAsync();
            return driver;
        }

        public async Task<DeliveryPerson> GetByUsernameAndPasswordAsync(string username, string password)
        {
            var driver = await _context.DeliveryPerson.FirstOrDefaultAsync(a => a.DriverName == username && a.Password == password);
            return driver;
        }

        public async Task<DeliveryPerson> GetDriver()
        {
            return await _context.DeliveryPerson.FirstOrDefaultAsync();
        }

        public async Task<DeliveryPerson> UpdateAvailabilityStatus(int driverid)
        {
            var driver = await _context.DeliveryPerson.FirstOrDefaultAsync(d => d.DeliveryPersonId == driverid);
            if (driver == null)
            {
                return null; 
            }
            driver.IsAvailable = !driver.IsAvailable;
            _context.DeliveryPerson.Update(driver);
            await _context.SaveChangesAsync();
            return driver;
        }

        public async Task<ICollection<Order>> GetAssignedOrders(int driverid)
        {
            var assignedOrders = await _context.Order.Include(o=> o.Customer).Include(o=>o.Restaurant).Include(o=>o.CartItems).Where(o => o.DeliveryPersonId == driverid).ToListAsync();
            return assignedOrders;
        }

        public async Task<string> AcceptOrDeclineOrder(int orderid, int driverid, Boolean acceptordecline)
        {
            var order = await _context.Order.FirstOrDefaultAsync(o => o.OrderId == orderid);
            if (order == null)
            {
                return "Order not found.";
            }
            order.IsAccept = acceptordecline;
            //order.DeliveryPersonId = driverid;
            if (order.IsAccept)
            {
                order.OrderStatus = 3;  // You can customize the status string as per your requirement
            }
            else
            {
                order.OrderStatus = 2;
            }
            _context.Order.Update(order);
            await _context.SaveChangesAsync();
            return order.IsAccept ? "Order accepted successfully." : "Order declined successfully.";

        }


        public async Task<ICollection<Order>> GetAcceptedOrders(int driverid)
        {
            var acceptedOrders = await _context.Order
                .Include(o => o.Customer)
                .Include(o => o.Restaurant)
                .Include(o => o.CartItems)
                .Where(o => o.DeliveryPersonId == driverid && o.IsAccept == true).ToListAsync();

                
            return acceptedOrders;
        }


        public async Task<ICollection<Order>> GetDeliveredOrders(int driverid)
        {
            var deliveredOrders = await _context.Order.Include(o => o.Customer).Include(o => o.Restaurant).Include(o => o.CartItems).Where(o => o.DeliveryPersonId == driverid && o.IsAccept == true && o.OrderStatus == 4).ToListAsync();
            return deliveredOrders;
        }
        public async Task<Order> GetOrderByid(int orderid)
        {
            var orderDetails = await _context.Order
            .Include(o => o.DeliveryPerson) // Include related DeliveryPerson data
            .Include(o => o.Restaurant) // Include related Restaurant data
            .Include(o => o.Customer) // Include related Customer data
            .Include(o => o.CartItems)
            .FirstOrDefaultAsync(o => o.OrderId == orderid);
            return orderDetails;
        }

        public async Task<object> GetOrderDetails(int orderid)
        {
           var orderDetails = await _context.Order
        .Where(o => o.OrderId == orderid)
        .Include(o => o.DeliveryPerson)
        .Include(o => o.Restaurant)
        .Include(o => o.Customer)
        .Include(o => o.CartItems)
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
            o.pickupLocation,
            o.dropoffLocation,
            o.pickupCity,
            o.dropoffCity,
            o.CreatedAt,
            o.UpdatedAt,
            Customer = new
            {
                o.Customer.UserId,
                o.Customer.FirstName,
                o.Customer.LastName,
                o.Customer.Email,
                o.Customer.PhoneNumber,
                o.Customer.Location
            },
            Restaurant = new
            {
                o.Restaurant.RestaurantId,
                o.Restaurant.Name,
                o.Restaurant.Location,
                o.Restaurant.City,
                o.Restaurant.Country
            },
            DeliveryPerson = o.DeliveryPerson == null ? null : new
            {
                o.DeliveryPerson.DeliveryPersonId,
                o.DeliveryPerson.DriverName,
                o.DeliveryPerson.DriverEmail,
                o.DeliveryPerson.Phone,
                o.DeliveryPerson.IsAvailable
            },
            CartItems = o.CartItems.Select(ci => new
            {
                ci.CartId,
                ci.ItemId,
                ci.Quantity,
                ci.Items
            }).ToList()
        })
        .FirstOrDefaultAsync();

    if (orderDetails == null)
    {
        throw new Exception("Order not found.");
    }

    return orderDetails;
        }


       public async Task<DeliveryPerson> GetDriverById(int driverid)
        {
            return await _context.DeliveryPerson.Where(d => d.DeliveryPersonId == driverid).FirstOrDefaultAsync();
        }
        public async Task<string> UpdateOrderStatus(int orderid)
        {
            var order = await _context.Order.FirstOrDefaultAsync(o => o.OrderId == orderid);
            if (order == null)
            {
                return "Order not found.";
            }
            order.OrderStatus = 4;
            _context.Order.Update(order);
            await _context.SaveChangesAsync();
            return "Order status updated";
        }



        public async Task<DeliveryPerson> GetDriverForLogin(string username)
        {
            var driver = await _context.DeliveryPerson.FirstOrDefaultAsync(d=>d.DriverName== username );
            return driver;
        }



    }
}
