using Microsoft.EntityFrameworkCore;
using System;
using waytodine_sem9.Controllers.restaurant;
using waytodine_sem9.Models.restaurant;
using waytodine_sem9.Repositories.restaurant.resInterfaces;
using waytodine_sem9.Services.restaurant.resInterfaces;

namespace waytodine_sem9.Services.restaurant.resClasses
{
    public class OderService : IOrderService
    {
        private readonly IResRepository _resRepository;
        public OderService(IResRepository resRepository)
        {
            _resRepository = resRepository;
        }

        public async Task<Order> Updatewithdelierypersonid(int orderid, int driverid)
        {
            return await _resRepository.UpdateOrder(orderid, driverid);
            ;
        }
        public async Task<Order> updatestatus(int orderid)
        {
            return await _resRepository.UpdateStatus(orderid);

        }


    }
}
