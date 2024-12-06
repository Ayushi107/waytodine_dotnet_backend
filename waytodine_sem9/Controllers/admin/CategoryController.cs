using Microsoft.AspNetCore.Mvc;
using waytodine_sem9.Services.admin.adminClasses;
using waytodine_sem9.Services.admin.adminInterfaces;

namespace waytodine_sem9.Controllers.admin
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController:ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("get-categories")]
        public async Task<IActionResult> GetAllCategories([FromBody] PaginationCatDto paginationDto)
        {
            var categories = await _categoryService.GetAllCategoriesAsync(paginationDto.PageNumber, paginationDto.PageSize);
            return Ok(categories);
        }

        //[HttpPost("get-category-by-id")]
        //public async Task<IActionResult> GetCategoryById([FromBody] GetCategoryByIdDto getCategoryByIdDto)
        //{
        //    var category = await _categoryService.GetCategoryByIdAsync(getCategoryByIdDto);
        //    if (category == null)
        //    {
        //        return NotFound("Category not found.");
        //    }
        //    return Ok(category);
        //}


        [HttpPost("add-category")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest("All the fields are required.");
            }

            var cat = await _categoryService.CreateCategoryAsync(categoryDto);
            if (cat == null)
            {
                return StatusCode(500, "An error occurred while creating the admin.");
            }

            return Ok(new { Message = "Category added successfully.", Category = cat });
        }


        [HttpPut("update-category")]
        public async Task<IActionResult> UpdateAdmin([FromBody] CategoryUpdateDto categoryUpdateDto)
        {
            if (categoryUpdateDto == null || categoryUpdateDto.Id <= 0)
            {
                return BadRequest("Invalid Category data.");
            }

            var updatedCategory = await _categoryService.UpdateCategoryAsync(categoryUpdateDto);
            if (updatedCategory == null)
            {
                return NotFound("category not found.");
            }

            return Ok(new { Message = "Category updated successfully.", Category = updatedCategory });
        }


        [HttpPost("delete-category")]
        public async Task<IActionResult> DeleteCategory(GetCategoryByIdDto getCategoryByIdDto)
        {
            var result = await _categoryService.DeleteCategoryAsync(getCategoryByIdDto);
            if (result)
            {
                return Ok(new { Message = "Category deleted successfully." });
            }
            return NotFound("Category not found.");
        }


        public class PaginationCatDto
        {
            public int PageNumber { get; set; }
            public int PageSize { get; set; }
        }

        public class GetCategoryByIdDto
        {
            public int Id { get; set; }
        }
        public class CategoryDto
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int Status { get; set; }
            public string Image {  get; set; }
            //public int RestaurantId { get; set; }
        }

        public class CategoryUpdateDto
        {
            public int Id { get; set; } // Admin ID
            public string Name { get; set; }
            public string Description { get; set; }
            //public int RestaurantId { get; set; }
        }
    }
}
