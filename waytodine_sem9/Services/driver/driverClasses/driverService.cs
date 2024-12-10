using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using waytodine_sem9.Controllers.admin;
using waytodine_sem9.Models.admin;
using waytodine_sem9.Repositories.admin.adminClasses;
using waytodine_sem9.Repositories.admin.adminInterfaces;
using waytodine_sem9.Repositories.driver.driverInterfaces;
using waytodine_sem9.Services.admin.adminInterfaces;
using static System.Net.WebRequestMethods;
using waytodine_sem9.Services.driver.driverInterfaces;
using waytodine_sem9.Controllers.driver;

namespace waytodine_sem9.Services.driver.driverClasses
{
    public class driverService:IdriverService
    {
        private readonly IdriverRepository _driverRepository;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly IMemoryCache _memoryCache;
        private string _otp;

        public driverService(IdriverRepository driverRepository, IConfiguration configuration, IEmailService emailService, IMemoryCache memoryCache)
        {
            _driverRepository = driverRepository;
            _configuration = configuration;
            _emailService = emailService;
            _memoryCache = memoryCache;
        }


        public async Task<DeliveryPerson> CreateDeliveryPersonAsync(DriverRegisterDto deliveryPerson)
        {
            return await _driverRepository.AddDriver(deliveryPerson);
        }

        public async Task<object> LoginAsync(string username, string password)
        {
            var driver = await _driverRepository.GetByUsernameAndPasswordAsync(username, password);
            if (driver == null)
            {
                return null; // Username or password is incorrect
            }

            // Generate JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, driver.DriverName),
                    new Claim(ClaimTypes.Role, "Driver")
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //return tokenHandler.WriteToken(token);
            return new
            {
                Data = driver,
                Token = tokenHandler.WriteToken(token)
            };
        }

        public async Task<DeliveryPerson> GetDriverByIdAsync(int driverid)
        {
            return await _driverRepository.GetDriverById(driverid);
        }
        public async Task<DeliveryPerson> GetProfileAsync()
        {
            return await _driverRepository.GetDriver();
        }

       

        public async Task<DeliveryPerson> GetDriverAsync()
        { 
            return await _driverRepository.GetDriver();
        }
        public async Task<DeliveryPerson> UpdateAvailabilityStatusAsync(int driverid)
        {
            return await _driverRepository.UpdateAvailabilityStatus(driverid);
        }
        public async Task<ICollection<Order>> GetAssignedOrdersAsync(int driverid)
        {
            return await _driverRepository.GetAssignedOrders(driverid);
        }
        public async Task<string> AcceptOrDeclineOrderAsync(int orderid, int driverid, Boolean acceptordecline)
        {
            return await _driverRepository.AcceptOrDeclineOrder(orderid, driverid, acceptordecline);
        }
        public async Task<ICollection<Order>> GetAcceptedOrdersAsync(int driverid)
        {
            return await _driverRepository.GetAcceptedOrders(driverid);
        }
        public async Task<ICollection<Order>> GetDeliveredOrdersAsync(int driverid)
        {
            return await _driverRepository.GetDeliveredOrders(driverid);
        }
        public async Task<object> GetOrderDetailsAsync(int orderid)
        {
            return await _driverRepository.GetOrderDetails(orderid);
        }

        public async Task<string> GenerateAndSendOTP(int orderid)
        {
            var order = await _driverRepository.GetOrderByid(orderid);
            var email = order.Customer.Email;
            if (order == null  || order.Customer == null)
            {
                return "Order or Customer not found"; 
            }
            var random = new Random();
            _otp = random.Next(100000, 999999).ToString();
            _memoryCache.Set($"Otp_{email}", _otp, TimeSpan.FromMinutes(10));
            await _emailService.SendEmailAsync(email, "Your OTP Code", $"Your OTP is: {_otp}");
            return "OTP send successfully";

        }

        public async Task<bool> VerifyOtpAndConfirmDelivery(int orderid, int driverid, string enteredOtp)
        {
            var order = await _driverRepository.GetOrderByid(orderid);
            var email = order.Customer.Email;
            if (order == null || order.Customer == null)
            {
                return false;
            }

            if (_memoryCache.TryGetValue($"Otp_{email}", out string cachedOtp) && cachedOtp == enteredOtp)
            {
                var result = await _driverRepository.UpdateOrderStatus(orderid);
                // Remove OTP after successful verification to avoid reuse
                _memoryCache.Remove($"Otp_{email}");
                if(result != null )
                {
                    return true;
                }
                return false;
            }
            return false;
        }



}
}
