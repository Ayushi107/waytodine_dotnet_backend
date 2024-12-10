using waytodine_sem9.Controllers.driver;
using waytodine_sem9.Models.admin;

namespace waytodine_sem9.Services.driver.driverInterfaces
{
    public interface IdriverService
    {
        Task<object> LoginAsync(string username, string password);
        Task<DeliveryPerson> GetDriverByIdAsync(int driverid);
        Task<DeliveryPerson> GetProfileAsync();
        Task<string> GenerateAndSendOTP(int orderid);
        Task<DeliveryPerson> GetDriverAsync();
        Task<DeliveryPerson> UpdateAvailabilityStatusAsync(int driverid);
        Task<ICollection<Order>> GetAssignedOrdersAsync(int driverid);
        Task<string> AcceptOrDeclineOrderAsync(int orderid, int driverid, Boolean acceptordecline);
        Task<ICollection<Order>> GetAcceptedOrdersAsync(int driverid);
        Task<ICollection<Order>> GetDeliveredOrdersAsync(int driverid);
        Task<object> GetOrderDetailsAsync(int orderid);
        Task<bool> VerifyOtpAndConfirmDelivery(int orderid, int driverid, string enteredOtp);

        //Task<object> GetDriverForLoginAsync(string username);


    }
}
