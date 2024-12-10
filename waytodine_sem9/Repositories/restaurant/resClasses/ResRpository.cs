using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;
using waytodine_sem9.Controllers.restaurant;
//using waytodine_sem9.Data;
using waytodine_sem9.MansiData;
using waytodine_sem9.Models.restaurant;
using waytodine_sem9.Repositories.restaurant.resInterfaces;

namespace waytodine_sem9.Repositories.restaurant.resClasses
{
    public class ResRepository : IResRepository
    {
        private readonly ApplicationDbContextMansi _context;
        private readonly Cloudinary _cloudinary;


        public ResRepository(ApplicationDbContextMansi context, Cloudinary cloudinary)
        {
            _context = context;
            _cloudinary = cloudinary;
        }
        public async Task<Restaurant> GetRestaurantByEmailAsync(string email)
        {
            return await _context.Restaurants.FirstOrDefaultAsync(r => r.Email == email);
        }
        public string SaveProfilePicFromBase64(string base64Image, string originalFileName)
        {


            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64Image);

                // Use a MemoryStream to upload the image to Cloudinary
                using (var stream = new MemoryStream(imageBytes))
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(originalFileName, stream),
                        PublicId = $"{originalFileName}_{DateTime.UtcNow.Ticks}", // Optional: Custom Public ID for Cloudinary
                        Overwrite = true // If you want to overwrite existing images with the same PublicId
                    };

                    var uploadResult = _cloudinary.Upload(uploadParams);

                    if (uploadResult?.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        // Return the secure URL of the uploaded image from Cloudinary
                        return uploadResult.SecureUrl.ToString(); // This is the URL for use in your database
                    }
                    else
                    {
                        throw new Exception("Failed to upload image to Cloudinary.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle Cloudinary upload exceptions
                Console.WriteLine($"Error uploading image to Cloudinary: {ex.Message}");
                throw new Exception("Failed to upload image to Cloudinary.");
            }

        }

        public async Task<List<Restaurant>> GetAllRestaurants()
        {
            return await _context.Restaurants.ToListAsync();
        }
        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }
        public async Task<List<MenuItem>> GetAllMenuitems()
        {
            return await _context.MenuItem.ToListAsync();
        }
        public async Task<List<MenuItem>> GetMenuItemsByRestaurantId(int restaurantId)
        {
            return await _context.MenuItem
                                 .Where(item => item.RestaurantId == restaurantId)
                                 .ToListAsync();
        }

        public async Task<List<Order>> GetAllOrders(int resid)
        {
            return await _context.Order.Where(o => o.RestaurantId == resid && o.IsAccept == true).ToListAsync();
        }
        public async Task<List<Order>> Getalloutfordeliveryorders(int resid)
        {
            return await _context.Order.Where(o => o.OrderStatus == 3 && o.RestaurantId == resid).ToListAsync();
        }

        public async Task<List<DeliveryPerson>> GetAllDeliveryPerson()
        {
            return await _context.DeliveryPersons.ToListAsync();
        }

        public async Task<List<Feedback>> GetrestaurantFeedbacks(int id)
        {
            return await _context.Feedbacks.Where(dp => dp.RestaurantId == id).ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {

            return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id); // Fetch category by ID
        }
        public async Task<User> GetUserById(int id)
        {

            return await _context.Users.FirstOrDefaultAsync(c => c.UserId == id); // Fetch category by ID
        }
        public async Task<DeliveryPerson> GetDeliveryPersonById(int id)
        {
            return await _context.DeliveryPersons.FirstOrDefaultAsync(c => c.DeliveryPersonId == id); // Fetch category by ID

        }
        public async Task<bool> AssignDriverToOrderAsync(int orderId, int driverId)
        {
            var order = await GetOrderById(orderId);

            if (order == null)
            {
                return false; // Order not found
            }

            // Update the order with the new driver and set status to 'On Delivery'
            order.DeliveryPersonId = driverId;
            order.OrderStatus = 3; // 'On Delivery'

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false; // Handle exception (e.g., log it)
            }
        }


        public async Task<Order> GetOrderById(int id)
        {
            return await _context.Order.FirstOrDefaultAsync(c => c.OrderId == id); // Fetch category by ID

        }
        public async Task<Restaurant> GetRestaurantById(int id)
        {
            return await _context.Restaurants.FirstOrDefaultAsync(c => c.RestaurantId == id);
        }


        public async Task<RestaurantDetails> GetRestaurantDetailsById(int restaurantId)
        {
            return await _context.RestaurantDetails.FirstOrDefaultAsync(c => c.RestaurantId == restaurantId);
        }
        public async Task<List<DeliveryPerson>> GetFreeDeliveryPersons()
        {
            return await _context.DeliveryPersons
                                 .Where(dp => dp.IsAvailable == true) // Filter by IsAvailable == true
                                 .ToListAsync();
        }
        public async Task<List<Order>> GetOrderDeliverypersonAssigned()
        {
            return await _context.Order
                                  .Where(dp => dp.OrderStatus == 3 && dp.DeliveryPersonId != null)
                                  .ToListAsync();
        }
        public async Task<List<Order>> getordercompleted(int resid)
        {
            return await _context.Order
                                  .Where(dp => dp.OrderStatus == 4 && dp.RestaurantId == resid)
                                  .ToListAsync();
        }
        public async Task<List<Order>> Getallisacceptorder(int resid)
        {
            return await _context.Order.Where(o => o.IsAccept == true && o.RestaurantId == resid).ToListAsync();
        }


        public async Task<Category> AddCategory(Category category)
        {
            //category.CategoryId = 0; // This ensures EF Core knows to auto-generate the value
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }
        public async Task<MenuItem> AddMenuItem(MenuItem menuItem, int resid)
        {
            string imageFileName = SaveProfilePicFromBase64(menuItem.ItemImage, menuItem.Name);
            menuItem.ItemId = 0; // Ensure EF Core auto-generates the ID
            menuItem.RestaurantId = resid;
            menuItem.ItemImage = imageFileName;
            _context.MenuItem.Add(menuItem);
            await _context.SaveChangesAsync();
            return menuItem;
        }

        // Method to get an item by ID
        public async Task<MenuItem> GetItemById(int id)
        {
            return await _context.MenuItem
                .FirstOrDefaultAsync(i => i.ItemId == id);
        }
        public async Task<bool> DeleteMenuItem(int itemId)
        {
            // Find the menu item by its ID
            var menuItem = await _context.MenuItem.FirstOrDefaultAsync(i => i.ItemId == itemId);

            // If the item doesn't exist, return false
            if (menuItem == null)
            {
                return false;
            }

            // Remove the menu item from the context
            _context.MenuItem.Remove(menuItem);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return true to indicate that the deletion was successful
            return true;
        }

        public async Task<Order> UpdateOrder(int orderid, int driverid)
        {

            //update order with status=3 as out for delivery and delivery person id in order table
            var order = await _context.Order.FirstOrDefaultAsync(o => o.OrderId == orderid);
            if (order == null)
            {
                throw new KeyNotFoundException("Order not found.");
            }

            // Update the order's delivery person ID and status
            order.DeliveryPersonId = driverid;
            order.OrderStatus = 3;
            _context.Order.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }
        public async Task<Order> UpdateOrderReady(int orderid)
        {
            // Find the order by orderId
            var order = await _context.Order.FirstOrDefaultAsync(o => o.OrderId == orderid);

            // If the order doesn't exist, throw an exception
            if (order == null)
            {
                throw new KeyNotFoundException("Order not found.");
            }
            //1 = placed, 2 = preparing, 3 = out for delivery means  order is ready , 4 = delivered 


            // Update only the IsAccept field
            order.OrderStatus = 3;

            // Save the changes
            await _context.SaveChangesAsync();

            return order;
        }


        public async Task<Order> UpdateStatus(int orderid)
        {
            // Find the order by orderId
            var order = await _context.Order.FirstOrDefaultAsync(o => o.OrderId == orderid);

            // If the order doesn't exist, throw an exception
            if (order == null)
            {
                throw new KeyNotFoundException("Order not found.");
            }
            //1 = placed, 2 = preparing, 3 = out for delivery, 4 = delivered


            // Update only the IsAccept field
            order.OrderStatus = 2;

            // Save the changes
            await _context.SaveChangesAsync();

            return order;
        }



        public async Task<List<RestaurantDTO>> GetOrdersByRestaurantAsync(int restaurantId)
        {
            var groupedOrders = await _context.Carts
                .Where(c => c.restaurantId == restaurantId)
                .Include(c => c.Restaurant)
                .Include(c => c.Customer)
                .Include(c => c.Order)
                .GroupBy(c => new { c.restaurantId, c.Restaurant.Name })
                .Select(group => new RestaurantDTO
                {
                    RestaurantId = group.Key.restaurantId,
                    RestaurantName = group.Key.Name,
                    Orders = group.GroupBy(o => o.OrderId) // Group by OrderId
                                  .Select(orderGroup => new OrderDTO
                                  {
                                      CartId = orderGroup.First().CartId,
                                      OrderId = orderGroup.Key, // Use OrderId as the grouping key
                                      UserId = orderGroup.First().userId,
                                      Username = orderGroup.First().Customer.FirstName,
                                      CreatedAt = orderGroup.First().CreatedAt,
                                      TotalPrice = orderGroup.Sum(o => o.totalPrice),
                                      OrderStatus = orderGroup.First().status,
                                      PaymentStatus = orderGroup.First().status,
                                      IsAccept = orderGroup.First().Order.IsAccept,// Assuming PaymentStatus is stored similarly
                                      Items = orderGroup.Select(i => new OrderItemDTO
                                      {
                                          ItemId = i.itemId,
                                          Quantity = i.quantity,
                                          Price = i.totalPrice
                                      }).ToList()
                                  }).ToList()
                }).ToListAsync();

            return groupedOrders;
        }

        public async Task<RestaurantDetails> UpdateRestaurantDetailsAsync(RestaurantDetails details)
        {
            string imageFileName = SaveProfilePicFromBase64(details.BannerImage, details.Description);
            details.BannerImage = imageFileName;
            _context.RestaurantDetails.Update(details);
            await _context.SaveChangesAsync();
            return details;
        }
        public async Task<Restaurant> UpdateRestaurant(Restaurant restaurant)
        {
            //string imageFileName = SaveProfilePicFromBase64(restaurant.RestaurantDocument,restaurant.Name);
            //restaurant.RestaurantDocument = imageFileName;
            _context.Restaurants.Update(restaurant);
            await _context.SaveChangesAsync();
            return restaurant;
        }

        public async Task<MenuItem> UpdateMenuitem(MenuItemUpdateDto menuItemDto)
        {
            var existingItem = await _context.MenuItem.FirstOrDefaultAsync(m => m.ItemId == menuItemDto.itemid);
            if (existingItem == null)
            {
                throw new KeyNotFoundException("Menu item not found.");
            }

            // Check if the provided CategoryId exists in the Category table
            if (menuItemDto.CategoryId > 0)
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == menuItemDto.CategoryId);
                if (category == null)
                {
                    throw new KeyNotFoundException("Category not found.");
                }
            }
            string imageFileName = SaveProfilePicFromBase64(menuItemDto.itemImage, menuItemDto.itemname);
            menuItemDto.itemImage = imageFileName;
            // Map the DTO to the MenuItem entity
            existingItem.Name = menuItemDto.itemname;
            existingItem.Description = menuItemDto.Description;
            existingItem.IsVeg = menuItemDto.isveg;
            existingItem.Price = menuItemDto.price;
            existingItem.Status = menuItemDto.status;

            // If CategoryId is provided, update it
            if (menuItemDto.CategoryId > 0)
            {
                existingItem.CategoryId = menuItemDto.CategoryId;
            }

            _context.MenuItem.Update(existingItem);
            await _context.SaveChangesAsync();

            return existingItem;
        }



        public async Task<bool> AcceptOrder(int orderId)
        {
            var order = await _context.Order.FindAsync(orderId);
            if (order == null)
            {
                return false;
            }

            order.IsAccept = true;
            order.UpdatedAt = DateTime.UtcNow;

            _context.Order.Update(order);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
