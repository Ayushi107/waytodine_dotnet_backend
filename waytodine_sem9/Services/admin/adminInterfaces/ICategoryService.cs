using waytodine_sem9.Models.admin;
using static waytodine_sem9.Controllers.admin.CategoryController;

namespace waytodine_sem9.Services.admin.adminInterfaces
{
    public interface ICategoryService
    {
        Task<object> GetAllCategoriesAsync(int pageNumber, int pageSize);
        Task<Category> GetCategoryByIdAsync(GetCategoryByIdDto getCategoryByIdDto);
        Task<bool> CreateCategoryAsync(CategoryDto categoryDto);
        Task<bool> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto);
        Task<bool> DeleteCategoryAsync(GetCategoryByIdDto getCategoryByIdDto);



        // change the paramter to dto
    }
}
