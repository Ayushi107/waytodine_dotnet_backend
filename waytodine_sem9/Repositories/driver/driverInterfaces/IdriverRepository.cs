using System.Collections.ObjectModel;
using waytodine_sem9.Models.admin;

namespace waytodine_sem9.Repositories.driver.driverInterfaces
{
    public interface IdriverRepository
    {
        Task<DeliveryPerson> AddDriver(DeliveryPerson driver);
        Task<DeliveryPerson> GetByUsernameAndPasswordAsync(string username, string password);
        Task<DeliveryPerson> GetDriver();
        Task<DeliveryPerson> UpdateAvailabilityStatus(int driverid);
        Task<ICollection<Order>> GetAssignedOrders(int driverid);
        Task<string> AcceptOrDeclineOrder(int orderid, int driverid, Boolean acceptordecline);
        Task<ICollection<Order>> GetAcceptedOrders(int driverid);
        Task<ICollection<Order>> GetDeliveredOrders(int driverid);
        Task<Order> GetOrderDetails(int orderid);
        Task<DeliveryPerson> GetDriverById(int driverid);
        Task<string> UpdateOrderStatus(int orderid);
    }
}
