using Microsoft.EntityFrameworkCore;
using waytodine_sem9.Models.restaurant;
using waytodine_sem9.Repositories.restaurant.resInterfaces;
using waytodine_sem9.Services.restaurant.resInterfaces;

namespace waytodine_sem9.Services.restaurant.resClasses
{
    public class ResService : IResService
    {
        private readonly IResRepository _resRepository;
        private readonly IConfiguration _configuration;

        public ResService(IResRepository resRepository, IConfiguration configuration)
        {
            _resRepository = resRepository;
            _configuration = configuration;
        }
       
        //public async Task<List<Feedback>> GetallrestaurantFeedbacks()
        //{

        //}
    }

}
