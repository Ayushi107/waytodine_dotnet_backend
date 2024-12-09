using waytodine_sem9.Controllers.restaurant;
using waytodine_sem9.Models.restaurant;
using static waytodine_sem9.Controllers.restaurant.ResController;

namespace waytodine_sem9.Services.restaurant.resInterfaces
{
    public interface IOrderService
    {
        Task<Order> Updatewithdelierypersonid(int orderid,int driverid);
        Task<Order> updatestatus(int orderid);
    }

}
