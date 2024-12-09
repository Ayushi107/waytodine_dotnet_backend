using waytodine_sem9.Controllers.restaurant;
using waytodine_sem9.Models.restaurant;
using waytodine_sem9.Repositories.restaurant.resInterfaces;
using waytodine_sem9.Services.restaurant.resInterfaces;

namespace waytodine_sem9.Services.restaurant.resClasses
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IResRepository _resRepository;
        public MenuItemService(IResRepository resRepository)
        {
            _resRepository = resRepository;
        }

        public async Task<MenuItem> AddMenuItemAsync(MenuItemDto menuItemDto, int resid)
        {
            // Validate the CategoryId to ensure it exists
            var category = await _resRepository.GetCategoryById(menuItemDto.CategoryId);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {menuItemDto.CategoryId} not found.");
            }

            // Create a new MenuItem instance
            var menuItem = new MenuItem
            {
                Name = menuItemDto.itemname,
                CategoryId = menuItemDto.CategoryId,
                Price = menuItemDto.price,
                Description = menuItemDto.Description,
                IsVeg = menuItemDto.isveg,
                Status = menuItemDto.status,
                ItemImage = menuItemDto.itemImage,

            };

            // Save the menu item using the repository
            var createdMenuItem = await _resRepository.AddMenuItem(menuItem, resid);
            return createdMenuItem;
        }
    }
}
