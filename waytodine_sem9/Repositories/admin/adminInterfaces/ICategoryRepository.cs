using waytodine_sem9.Models.admin;
using static waytodine_sem9.Controllers.admin.CategoryController;

namespace waytodine_sem9.Repositories.admin.adminInterfaces
{
    public interface ICategoryRepository
    {
        Task<object> GetAllCategories(int pageNumber, int pageSize);
        Task<Category> GetCategoryById(int id);
        Task<bool> CreateCategory(CategoryDto categoryDto);
        Task<bool> UpdateCategory(Category category);
        Task<bool> DeleteCategory(int id);
    }
}
