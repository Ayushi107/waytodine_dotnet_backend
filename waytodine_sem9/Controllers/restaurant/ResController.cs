using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using waytodine_sem9.Models.restaurant;
using waytodine_sem9.Repositories.restaurant.resInterfaces;
using waytodine_sem9.Services.restaurant.resClasses;
using waytodine_sem9.Services.restaurant.resInterfaces;

namespace waytodine_sem9.Controllers.restaurant
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResController : ControllerBase
    {
        private readonly IResRepository _resRepository;
        private readonly IMenuItemService _menuItemService;
        

        public ResController(IResRepository resRepository, IMenuItemService menuItemService)
        {
            _resRepository = resRepository;
            _menuItemService = menuItemService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Restaurant>>> GetAllRestaurants()
        {
            var restaurants = await _resRepository.GetAllRestaurants();
            return Ok(restaurants);
        }
        [HttpGet("categories")]
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            var categories = await _resRepository.GetAllCategories();
            return Ok(categories);
        }
        [HttpGet("alldeliveryperson")]
        public async Task<ActionResult<List<DeliveryPerson>>> GetAlldeliveryPerson()
        {
            var persons = await _resRepository.GetAllDeliveryPerson();
            return Ok(persons);
        }
        [HttpGet("menuitems")]
        public async Task<ActionResult<List<MenuItem>>> GetAllMenuitems()
        {
            var menuitems = await _resRepository.GetAllMenuitems();
            return Ok(menuitems);
        }
        [HttpGet("menuitems/{restaurantId}")]
        public async Task<ActionResult<List<MenuItem>>> GetMenuItemsByRestaurantId(int restaurantId)
        {
            // Fetch menu items filtered by restaurantId
            var menuitems = await _resRepository.GetMenuItemsByRestaurantId(restaurantId);

            if (menuitems == null || !menuitems.Any())
            {
                return NotFound("No menu items found for this restaurant.");
            }

            return Ok(menuitems);
        }

        [HttpGet("allordersbyresid/{resid}")]
        public async Task<ActionResult<List<Order>>> GetAllOrders(int resid)
        {
            var orders = await _resRepository.GetAllOrders(resid);
            return Ok(orders);
        }
        [HttpGet("alloutfordeliveryorders/{resid}")]
        public async Task<ActionResult<List<Order>>> GetAlloutfordeliveryOrders(int resid)
        {
            var orders = await _resRepository.Getalloutfordeliveryorders(resid);
            return Ok(orders);
        }

        [HttpGet("freedeliverypersons")]
        public async Task<ActionResult<DeliveryPerson>> GetFreeDeliveryPersons()
        {
            var freedeliverypersons = await _resRepository.GetFreeDeliveryPersons();
            return Ok(freedeliverypersons);
        }
        [HttpGet("getordercompleted/{resid}")]
        public async Task<ActionResult<List<Order>>> getordercompleted(int resid)
        {
            var freedeliverypersons = await _resRepository.getordercompleted(resid);
            return Ok(freedeliverypersons);
        }
        [HttpGet("getallisacceptorder/{resid}")]
        public async Task<ActionResult<List<Order>>> getallisacceptorder(int resid)
        {
            var isacceptorder = await _resRepository.Getallisacceptorder(resid);
            return Ok(isacceptorder);
        }
        [HttpGet("getallfeedbacks/{id}")]
        public async Task<ActionResult<List<Feedback>>> getallfeedbacks(int id)
        {
            var feedbacklist = await _resRepository.GetrestaurantFeedbacks(id);
            return Ok(feedbacklist);
        }
        [HttpGet("categories/{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            // Fetch category by ID from the repository
            var category = await _resRepository.GetCategoryById(id);

            if (category == null)
            {
                // Return 404 NotFound if category doesn't exist
                return NotFound($"Category with ID {id} not found");
            }

            return Ok(category); // Return the category if found
        }
        [HttpGet("MenuItem/{id}")]
        public async Task<ActionResult<MenuItem>> GetItemById(int id)
        {
            var item = await _resRepository.GetItemById(id);
            if (item == null)
            {
                return NotFound($"menuItem with ID {id} not found.");
            }
            return Ok(item);
        }
        [HttpGet("getuserbyid/{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {

            var user = await _resRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound($"user with ID {id} not found.");
            }
            return Ok(user);

        }
        [HttpGet("getdeliverypersonbyid/{id}")]
        public async Task<ActionResult<DeliveryPerson>> getdeliverypersonbyid(int id)
        {

            var person = await _resRepository.GetDeliveryPersonById(id);
            if (person == null)
            {
                return NotFound($"person with ID {id} not found.");
            }
            return Ok(person);

        }

        [HttpGet("getorderdeliverypersonassigned")]
        public async Task<ActionResult<List<Order>>> GetOrderDeliverypersonAssigned()
        {
            var orders = await _resRepository.GetOrderDeliverypersonAssigned();
            return Ok(orders);
        }
       

        [HttpPost("add-menu-item")]
        public async Task<ActionResult<MenuItem>> AddMenuItem([FromBody] MenuItemDto menuitemdto)
        {
            if (menuitemdto == null)
            {
                return BadRequest("Item data is required.");
            }

            // Validation logic...
            var addedItem = await _menuItemService.AddMenuItemAsync(menuitemdto, menuitemdto.RestaurantId);

            return CreatedAtAction(nameof(GetItemById), new { id = addedItem.ItemId }, addedItem);
        }

        [HttpDelete("deleteitem/{itemId}")]
        public async Task<IActionResult> DeleteMenuItem(int itemId)
        {
            // Call the DeleteMenuItem method from the repository
            var success = await _resRepository.DeleteMenuItem(itemId);

            // If the item was not found or not deleted, return a NotFound response
            if (!success)
            {
                return NotFound(new { message = "Menu item not found" });
            }

            // Return a successful response with a message
            return Ok(new { message = "Menu item deleted successfully" });
        }

        [HttpPut("assignDriverdto/{orderId}")]
        public async Task<IActionResult> AssignDriver([FromBody] AssignDriverDto request)
        {
            var ordertoupdate = await _resRepository.UpdateOrder(request.OrderId, request.DeliveryPersonId);
            return Ok(ordertoupdate);
        }
        [HttpPut("updatestatus/{orderId}")]
        public async Task<IActionResult> UpdateStatus(int orderId)
        {
            //var order = await _resRepository.GetOrderById(request.OrderId);
            var ordertoupdate = await _resRepository.UpdateStatus(orderId);
            return Ok(ordertoupdate);
        }
        [HttpPut("updatereadyorderstatus/{orderId}")]
        public async Task<IActionResult> UpdateReadyOrderStatus(int orderId)
        {
            //var order = await _resRepository.GetOrderById(request.OrderId);
            var ordertoupdate = await _resRepository.UpdateOrderReady(orderId);
            return Ok(ordertoupdate);
        }

        [HttpGet("getrestaurantbyid/{restaurantId}")]
        public async Task<IActionResult> GetRestaurantById(int restaurantId)
        {
            var res = await _resRepository.GetRestaurantById(restaurantId);
            return Ok(res);
        }
        [HttpGet("getrestaurantdetailsbyid/{restaurantId}")]
        public async Task<IActionResult> GetRestaurantdetailsById(int restaurantId)
        {
            var res = await _resRepository.GetRestaurantDetailsById(restaurantId);
            return Ok(res);
        }

        [HttpGet("GetOrdersByRestaurant/{restaurantId}")]
        public async Task<IActionResult> GetOrdersByRestaurant(int restaurantId)
        {
            try
            {
                var result = await _resRepository.GetOrdersByRestaurantAsync(restaurantId);
                if (result == null || result.Count == 0)
                {
                    return NotFound(new { message = "No orders found for this restaurant" });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }


        [HttpPut("Updaterestaurantdetails/{restaurantId}")]
        public async Task<IActionResult> UpdateRestaurantDetails([FromBody] RestaurantDetailsDto detailsDto, int restaurantId)
        {
            if (detailsDto == null)
                return BadRequest("Invalid data.");

            var details = await _resRepository.GetRestaurantDetailsById(restaurantId);
            if (details == null)
                return NotFound("Restaurant details not found.");

            details.BannerImage = detailsDto.BannerImage;
            details.CurrentOfferDiscountRate = detailsDto.CurrentOfferDiscountRate;
            details.Description = detailsDto.description;
            details.Mission = detailsDto.Mission;
            details.OpeningHoursWeekdays = detailsDto.OpeningHoursWeekdays;
            details.OpeningHoursWeekends = detailsDto.OpeningHoursWeekends;
            details.Specialities = detailsDto.Specialities;

            await _resRepository.UpdateRestaurantDetailsAsync(details);

            return Ok("Restaurant details updated successfully.");
        }
        [HttpPut("Updaterestaurant/{restaurantId}")]
        public async Task<IActionResult> UpdateRestaurant([FromBody] RestaurantDto resdto, int restaurantId)
        {
            if (resdto == null)
                return BadRequest("Invalid data.");

            var details = await _resRepository.GetRestaurantById(restaurantId);

            if (details == null)
                return NotFound("Restaurant details not found.");

            details.PhoneNumber = resdto.PhoneNumber;
            details.RestaurantDocument = resdto.RestaurantDocument;

            details.Email = resdto.Email;
            details.Location = resdto.Location;
            details.City = resdto.City;
            details.Country = resdto.Country;

            await _resRepository.UpdateRestaurant(details);

            return Ok("Restaurant updated successfully.");
        }
        [HttpPut("updateitem")]
        public async Task<IActionResult> UpdateMenuItem([FromBody] MenuItemUpdateDto menuItem)
        {
            // Ensure the itemId from the URL matches the one in the request body
           
            //MenuItem m = new MenuItem();
            //m.CategoryId = menuItem.CategoryId;
            //m.Price = menuItem.price;
            //m.Description = menuItem.Description;
            //m.Name = menuItem.itemname;
            //m.IsVeg = menuItem.isveg;
            //m.Status = menuItem.status;

            try
            {
                // Call repository method to update the item
                var updatedItem = await _resRepository.UpdateMenuitem(menuItem);

                // Return the updated item as confirmation
                return Ok(updatedItem);
            }
            catch (KeyNotFoundException ex)
            {
                // Handle case where item or category is not found
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Catch other exceptions and log the error
                return StatusCode(500, $"Error updating menu item: {ex.Message}");
            }
        }
        [HttpPut("assignDriver/{orderId}")]
        public async Task<IActionResult> AssignDriver(int orderId, [FromBody] AssignDriverRequest request)
        {
            if (request == null || request.DeliveryPersonId <= 0)
            {
                return BadRequest("Invalid driver information.");
            }
            var driver = await _resRepository.GetDeliveryPersonById(request.DeliveryPersonId);
            driver.IsAvailable = false;
            // Use the repository to assign the driver
            var isAssigned = await _resRepository.AssignDriverToOrderAsync(orderId, request.DeliveryPersonId);

            if (isAssigned)
            {
                return Ok(new { message = "Driver assigned and order status updated successfully." });
            }

            return NotFound("Order not found or failed to update.");
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { message = "Email and Password are required" });
            }

            // Use repository to get the restaurant
            var restaurant = await _resRepository.GetRestaurantByEmailAsync(request.Email);

            if (restaurant == null || restaurant.Password != request.Password)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            // Store username in session
            //HttpContext.Session.SetString("Username", restaurant.Name);

            // Return success response
            return Ok(new
            {
                message = "Login successful",
                restaurantId = restaurant.RestaurantId,
                username = restaurant.Name
            });
        }

    }
    public class AssignDriverRequest
    {
        public int DeliveryPersonId { get; set; }
    }
    public class RestaurantDto
    {
        public int RestaurantId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string RestaurantDocument { get; set; }
    }
    public class RestaurantDetailsDto
    {
        public int RestaurantId { get; set; }
        public string BannerImage { get; set; }
        public string description { get; set; }
        public double CurrentOfferDiscountRate { get; set; }
        public string Mission { get; set; }
        public string OpeningHoursWeekdays { get; set; }
        public string OpeningHoursWeekends { get; set; }
        public string Specialities { get; set; }
    }

    public class OrderItemDTO
    {
        public int ItemId { get; set; }
        //public string ItemName {  get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
    public class OrderDTO
    {
        public int CartId { get; set; }
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TotalPrice { get; set; }
        public bool IsAccept { get; set; }
        public int OrderStatus { get; set; }
        public int PaymentStatus { get; set; }
        public List<OrderItemDTO> Items { get; set; }
    }
    public class RestaurantDTO
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public List<OrderDTO> Orders { get; set; }
    }

    public class StatusChangeDto
    {
        public int OrderId { get; set; }
    }
    public class AssignDriverDto
    {
        public int DeliveryPersonId { get; set; }
        public int OrderId { get; set; }
    }
    public class CategoryAddDto
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int status { get; set; }
        public string categoryImage { get; set; }
    }
    public class MenuItemUpdateDto
    {
        public int itemid { get; set; }
        public string itemname { get; set; }
        public string Description { get; set; }
        public int isveg { get; set; }
        public int price { get; set; }
        public int status { get; set; }

        public int CategoryId { get; set; }
    }
    public class MenuItemDto
    {
        public string itemname { get; set; }
        public string Description { get; set; }
        public int isveg { get; set; }
        public string itemImage { get; set; }
        public int price { get; set; }
        public int status { get; set; }
        public int RestaurantId { get; set; }
        public int CategoryId { get; set; }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
