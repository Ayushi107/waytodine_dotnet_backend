using waytodine_sem9.Controllers.restaurant;
using waytodine_sem9.Models.restaurant;

namespace waytodine_sem9.Services.restaurant.resInterfaces
{
    public interface IMenuItemService
    {
        Task<MenuItem> AddMenuItemAsync(MenuItemDto MenuItemAddDto, int resid);
    }
}
