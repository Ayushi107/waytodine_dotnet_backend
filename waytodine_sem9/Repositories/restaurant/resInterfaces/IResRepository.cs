using Microsoft.AspNetCore.Mvc;
using waytodine_sem9.Controllers.restaurant;
using waytodine_sem9.Models.restaurant;

namespace waytodine_sem9.Repositories.restaurant.resInterfaces
{
    public interface IResRepository
    {
        Task<Restaurant> GetRestaurantByEmailAsync(string email);
        Task<List<Restaurant>> GetAllRestaurants();
        Task<List<Category>> GetAllCategories();
        Task<List<MenuItem>> GetAllMenuitems();
        Task<List<DeliveryPerson>> GetAllDeliveryPerson();
        Task<List<Order>> GetAllOrders(int resid);

        Task<List<Order>> Getallisacceptorder(int resid);
        Task<List<Order>> Getalloutfordeliveryorders(int resid);


        //by ID
        Task<Category> GetCategoryById(int id);
        Task<User> GetUserById(int id);
        Task<MenuItem> GetItemById(int id);
        Task<DeliveryPerson> GetDeliveryPersonById(int id);
        Task<Order> GetOrderById(int id);
        Task<bool> AssignDriverToOrderAsync(int orderId, int driverId);
        Task<Restaurant> GetRestaurantById(int id);
        //Task<RestaurantDetails> GetRestaurantDetailsByResId(int id);
        Task<RestaurantDetails> GetRestaurantDetailsById(int restaurantId);

        Task<List<Feedback>> GetrestaurantFeedbacks(int id);

        //ADD DATA
        Task<Category> AddCategory(Category category);
        Task<MenuItem> AddMenuItem(MenuItem menuItem, int resid);
        //update data
        Task<Order> UpdateOrder(int orderid, int driverid);
        Task<Order> UpdateStatus(int orderid);
        Task<Order> UpdateOrderReady(int orderid);
        Task<RestaurantDetails> UpdateRestaurantDetailsAsync(RestaurantDetails details);
        Task<Restaurant> UpdateRestaurant(Restaurant restaurant);
        Task<MenuItem> UpdateMenuitem(MenuItemUpdateDto menuItemDto);
        //getall with condition
        Task<List<Order>> GetOrderDeliverypersonAssigned();
        Task<List<DeliveryPerson>> GetFreeDeliveryPersons();
        Task<List<Order>> getordercompleted(int resid);
        //Task<List<RestaurantDTO>> GetAllOrdersGroupedByRestaurantAsync(int restaurantId);
        Task<List<RestaurantDTO>> GetOrdersByRestaurantAsync(int restaurantId);
        //Delete data
        Task<bool> DeleteMenuItem(int itemId);
        Task<List<MenuItem>> GetMenuItemsByRestaurantId(int restaurantId);

    }
}
