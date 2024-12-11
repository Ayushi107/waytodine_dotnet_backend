using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using waytodine_sem9.Models.admin;
using waytodine_sem9.Services.admin.adminClasses;
using waytodine_sem9.Services.admin.adminInterfaces;

namespace waytodine_sem9.Controllers.admin
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListingController:ControllerBase
    {
        private readonly IListingService _listingService;

        public ListingController(IListingService listingService)
        {
            _listingService = listingService;
        }

        [HttpPost("get-Restaurants")]
        public async Task<IActionResult> GetAllRestaurants([FromBody] PaginationDto paginationDto)
        {
            var restaurnats = await _listingService.GetAllRestaurantAsync(paginationDto.PageNumber,paginationDto.PageSize);
            //if (restaurnats == null)
            //{
            //    return BadRequest("No Restaurants Found");
            //}
            return Ok(restaurnats);
        }

        [HttpPost("get-orders")]
        [EnableCors("Allow")]
        public async Task<IActionResult> GetAllOrders([FromBody] PaginationDto paginationDto)
        {
            var orders = await _listingService.GetAllOrdersAsync(paginationDto.PageNumber, paginationDto.PageSize);
            //if (orders == null)
            //{
            //    return BadRequest("No Orders Found");
            //}
            return Ok(orders);
        }

        [HttpPost("get-Drivers")]
        public async Task<IActionResult> GetAllDrivers([FromBody] PaginationDto paginationDto)
        {
            var drivvers = await _listingService.GetAllDriversAsync(paginationDto.PageNumber, paginationDto.PageSize);
            if (drivvers == null)
            {
                return BadRequest("No Drivers Found");
            }
            return Ok(drivvers);
        }

        [HttpPost("get-Users")]
        [EnableCors("Allow")]

        public async Task<IActionResult> GetAllUsers([FromBody] PaginationDto paginationDto)
        {
            var users = await _listingService.GetAllUsersAsync(paginationDto.PageNumber, paginationDto.PageSize);
            //if (users == null)
            //{
            //    return BadRequest("No Users Found");
            //}
            return Ok(users);
        }

        [HttpPost("get-Menus")]
        public async Task<IActionResult> GetMenus([FromBody] PaginationDto paginationDto)
        {
            var users = await _listingService.GetAllMenusAsync(paginationDto.PageNumber, paginationDto.PageSize);
            if (users == null)
            {
                return BadRequest("No Menus Found");
            }
            return Ok(users);
        }

        [HttpPost("get-Restaurant-details")]
        [EnableCors("Allow")]

        public async Task<IActionResult> GetRestaurantDetails([FromBody] RestauarntIdDto restauarntIdDto)
        {
            var details = await _listingService.GetRestaurantDetailsByIdAsync(restauarntIdDto.RestaurantId);
            if (details == null)
            {
                return BadRequest("No RestauarntDEtails Found");
            }
            return Ok(details);
        }
    }
    public class PaginationDto
    {
       
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }

    public class RestauarntIdDto
    {

        public int RestaurantId { get; set; }
     
    }
}
