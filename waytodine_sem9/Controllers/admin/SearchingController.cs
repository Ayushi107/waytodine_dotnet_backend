using Microsoft.AspNetCore.Mvc;
using waytodine_sem9.Services.admin.adminInterfaces;

namespace waytodine_sem9.Controllers.admin
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchingController:ControllerBase
    {
        private readonly ISearchingService _searchingService;

        public SearchingController(ISearchingService searchingService)
        {
            _searchingService = searchingService;
        }

        [HttpPost("search-orders")]
        public async Task<IActionResult> SearchOrders([FromBody] SearchiOdersDto searchiOdersDto)
        {
            try
            {
                var response = await _searchingService.SearchOrdersAsync(searchiOdersDto.RestaurantName, searchiOdersDto.PageNumber, searchiOdersDto.PageSize);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the error
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost("search-menus")]
        public async Task<IActionResult> SearchMenus([FromBody] SearchiOdersDto searchiOdersDto)
        {
            try
            {
                var response = await _searchingService.SearchMenusAsync(searchiOdersDto.RestaurantName, searchiOdersDto.PageNumber, searchiOdersDto.PageSize);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the error
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        [HttpPost("search-users")]
        public async Task<IActionResult> SearchUsers([FromBody] SearchiOdersDto searchiOdersDto)
        {
            try
            {
                var response = await _searchingService.SearchUsersAsync(searchiOdersDto.RestaurantName, searchiOdersDto.PageNumber, searchiOdersDto.PageSize);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the error
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        [HttpPost("search-restaurants")]
        public async Task<IActionResult> SearchRestaurants([FromBody] SearchiOdersDto searchiOdersDto)
        {
            try
            {
                var response = await _searchingService.SearchREstaurantsAsync(searchiOdersDto.RestaurantName, searchiOdersDto.PageNumber, searchiOdersDto.PageSize);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the error
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

    }
    public class SearchiOdersDto
    {
        public string RestaurantName { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }
}
