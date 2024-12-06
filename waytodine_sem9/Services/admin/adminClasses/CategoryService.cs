using System.Data;
using waytodine_sem9.Models.admin;
using waytodine_sem9.Repositories.admin.adminClasses;
using waytodine_sem9.Repositories.admin.adminInterfaces;
using waytodine_sem9.Services.admin.adminInterfaces;
using static waytodine_sem9.Controllers.admin.CategoryController;

namespace waytodine_sem9.Services.admin.adminClasses
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<object> GetAllCategoriesAsync(int pageNumber, int pageSize)
        {
            return await _categoryRepository.GetAllCategories(pageNumber,pageSize);
        }

        public async Task<Category> GetCategoryByIdAsync(GetCategoryByIdDto getCategoryByIdDto)
        {
          

            return await _categoryRepository.GetCategoryById(getCategoryByIdDto.Id);
        }

        public async Task<bool> CreateCategoryAsync(CategoryDto categoryDto)
        {
            //var user = new UserEntity
            //{
            //    Email = "abc@gmail.com",
            //    FirstName = "abc",
            //    LastName = "abc",
            //    Password = "abc",
            //    PhoneNumber = "9898767545",
            //    Role = "User"
            //};
            //var restaurant = new Restaurant
            //{
            //    User = user,
            //    RestaurantName = "lemon",
            //    Description = "punjabi",
            //    LicenseNumber = "ertyui45678io",
            //    LicenseDocument = "5678vbn",
            //    BankAccountNumber = "234rfc",
            //    BankIfscCode = "4567890p",
            //    RazorpayAccountId = "4567890"
            //};
            var category = new Category
            {
                CategoryName = categoryDto.Name,
                Description = categoryDto.Description,
                //RestaurantId = categoryDto.RestaurantId
               
            };
            return await _categoryRepository.CreateCategory(category);
        }

        public async Task<bool> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto)
        {
            var category = new Category
            {
                CategoryId = categoryUpdateDto.Id,
                CategoryName = categoryUpdateDto.Name,
                Description = categoryUpdateDto.Description,

            };
            return await _categoryRepository.UpdateCategory(category);
        }

        public async Task<bool> DeleteCategoryAsync(GetCategoryByIdDto getCategoryByIdDto)
        {
            return await _categoryRepository.DeleteCategory(getCategoryByIdDto.Id);
        }
    }
    }
