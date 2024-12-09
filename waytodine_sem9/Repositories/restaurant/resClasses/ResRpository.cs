using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;
using waytodine_sem9.Controllers.restaurant;
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
        public async Task<List<Order>> GetAllOrders()
        {
            return await _context.Order.ToListAsync();
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

        public async Task<Order> GetOrderById(int id)
        {
            return await _context.Order.FirstOrDefaultAsync(c => c.OrderId == id); // Fetch category by ID

        }
        public async Task<Restaurant> GetRestaurantById(int id)
        {
            return await _context.Restaurants.FirstOrDefaultAsync(c => c.RestaurantId == id);
        }
        public async Task<RestaurantDetails> GetRestaurantDetailsById(int id)
        {
            return await _context.RestaurantDetails.FirstOrDefaultAsync(c => c.RestaurantId == id);
        }
        public async Task<List<DeliveryPerson>> GetFreeDeliveryPersons()
        {
            return await _context.DeliveryPersons
                                 .Where(dp => dp.IsAvailable==true) // Filter by IsAvailable == true
                                 .ToListAsync();
        }
        public async Task<List<Order>> GetOrderDeliverypersonAssigned()
        {
            return await _context.Order
                                  .Where(dp => dp.OrderStatus ==3  && dp.DeliveryPersonId != null)
                                  .ToListAsync();
        }
        public async Task<List<Order>> getordercompleted()
        {
            return await _context.Order
                                  .Where(dp => dp.OrderStatus == 4)
                                  .ToListAsync();
        }


        public async Task<Category> AddCategory(Category category)
        {
            //category.CategoryId = 0; // This ensures EF Core knows to auto-generate the value
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
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
        public async Task<MenuItem> GetItemById(int id, int resid)
        {
            return await _context.MenuItem
                .FirstOrDefaultAsync(i => i.ItemId == id && i.RestaurantId == resid);
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

        public async Task<Order> UpdateOrder(int orderid,int driverid)
        {
            var order = await _context.Order.FirstOrDefaultAsync(o=>o.OrderId== orderid);
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

        public async Task<Order> UpdateStatus(int orderid)
        {
            // Find the order by orderId
            var order = await _context.Order.FirstOrDefaultAsync(o => o.OrderId == orderid);

            // If the order doesn't exist, throw an exception
            if (order == null)
            {
                throw new KeyNotFoundException("Order not found.");
            }

            // Update only the IsAccept field
            order.IsAccept = true;

            // Ensure only the IsAccept field is marked as modified
            _context.Entry(order).Property(o => o.IsAccept).IsModified = true;

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
                .GroupBy(c => new { c.restaurantId, c.Restaurant.Name })
                .Select(group => new RestaurantDTO
                {
                    RestaurantId = group.Key.restaurantId,
                    RestaurantName = group.Key.Name,
                    Orders = group.GroupBy(o => o.CartId)
                                  .Select(orderGroup => new OrderDTO
                                  {
                                      OrderId = orderGroup.Key,
                                      UserId = orderGroup.First().userId,
                                      Username = orderGroup.First().Customer.FirstName,
                                      CreatedAt = orderGroup.First().CreatedAt,
                                      TotalPrice = orderGroup.Sum(o => o.totalPrice),
                                      OrderStatus = orderGroup.First().status,
                                      PaymentStatus = orderGroup.First().status, // Assuming PaymentStatus is stored similarly
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
            _context.RestaurantDetails.Update(details);
            await _context.SaveChangesAsync();
            return details;
        }
        public async Task<Restaurant> UpdateRestaurant(Restaurant restaurant)
        {
            _context.Restaurants.Update(restaurant);
            await _context.SaveChangesAsync();
            return restaurant;
        }


    }
}
