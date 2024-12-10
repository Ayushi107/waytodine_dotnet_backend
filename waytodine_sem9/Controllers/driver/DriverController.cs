using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using waytodine_sem9.Controllers.admin;
using waytodine_sem9.Models.admin;
using waytodine_sem9.Services.admin.adminClasses;
using waytodine_sem9.Services.admin.adminInterfaces;
using waytodine_sem9.Services.driver.driverInterfaces;

namespace waytodine_sem9.Controllers.driver
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController:ControllerBase
    {
        private readonly IdriverService _driverService;

        public DriverController(IdriverService driverService)
        {
            _driverService = driverService;
        }


        [HttpPost("register-driver")]
        public async Task<IActionResult> Register([FromBody] DriverRegisterDto registerDto)
        {
            if (registerDto == null)
            {
                return BadRequest("Data required.");
            }

            var driver = await _driverService.CreateDeliveryPersonAsync(registerDto);
            if (driver == null)
            {
                return StatusCode(500, "An error occurred while creating the admin.");
            }

            return Ok(new { Message = "Driver registered successfully.", Driver = driver });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] DriverLoginDto driverLoginDto)
        {
            var token = await _driverService.LoginAsync(driverLoginDto.Username, driverLoginDto.Password);
            //if (token == null)
            //{
            //    return Unauthorized("Invalid username or password.");
            //}
            return Ok(new { Token = token });
        }

        [HttpPost("get-profile")]
        public async Task<IActionResult> GetProfile([FromBody] DriverIdDto driverIdDto)
        {
            var driver = await _driverService.GetDriverByIdAsync(driverIdDto.DriverId);
            if(driver == null)
            {
                return NotFound();
            }
            return Ok(driver);
        }

        [HttpPost("update-availability-status")]
        public async Task<IActionResult> UpdateDriverAvailabilityStatus([FromBody] DriverIdDto driverIdDto)
        {
            var driver = await _driverService.UpdateAvailabilityStatusAsync(driverIdDto.DriverId);
            if (driver == null)
            {
                return BadRequest("Unable to update Driver Status.");
            }
            return Ok(driver);
        }


        [HttpPost("get-assigned-orders")]
        public async Task<IActionResult> GetAssignedOrders([FromBody] DriverIdDto driverIdDto)
        {
            var orders = await _driverService.GetAssignedOrdersAsync(driverIdDto.DriverId);
            return Ok(orders);
        }

        [HttpPost("accept-decline-order")]
        public async Task<IActionResult> AcceptOrDeclineOrder([FromBody] AcceptDeclineDto acceptDeclineDto)
        {
            var result = await _driverService.AcceptOrDeclineOrderAsync(acceptDeclineDto.OrderId, acceptDeclineDto.DriverId, acceptDeclineDto.AcceptOrDecline);
            return Ok(result);
        }

        [HttpPost("get-accepted-orders")]
        public async Task<IActionResult> GetAcceptedOrders([FromBody] DriverIdDto driverIdDto)
        {
            var orders = await _driverService.GetAcceptedOrdersAsync(driverIdDto.DriverId);
            return Ok(orders);
        }

        [HttpPost("get-delivered-orders")]
        public async Task<IActionResult> GetDeliveredOrders([FromBody] DriverIdDto driverIdDto)
        {
            var orders = await _driverService.GetDeliveredOrdersAsync(driverIdDto.DriverId);
            return Ok(orders);
        }

        [HttpPost("get-order-details")]
        public async Task<IActionResult> GetOrderDetails([FromBody] DriverIdDto orderId)
        {
            var order = await _driverService.GetOrderDetailsAsync(orderId.DriverId);
            if (order == null)
            {
                return NotFound("Order not found.");
            }
            return Ok(order);
        }

        [HttpPost("generate-otp")]
        public async Task<IActionResult> GenerateOtp([FromBody] DriverIdDto orderid)
        {
            var result = await _driverService.GenerateAndSendOTP(orderid.DriverId);
            return Ok(result);
        }

        [HttpPost("confirm-delivery")]
        public async Task<IActionResult> VerifyOtp([FromBody] OtpVerificationDto otpDto)
        {
            var result = await _driverService.VerifyOtpAndConfirmDelivery(otpDto.OrderId, otpDto.DriverId, otpDto.EnteredOtp);
            if (!result)
            {
                return BadRequest("OTP verification failed or delivery confirmation unsuccessful.");
            }
            return Ok("Delivery confirmed successfully.");
        }



    }

    public class DriverRegisterDto
    {
       public string VehicleType {  get; set; }
            public string VehicleNumber { get; set; }
        public string DrivingLicenseNumber { get; set; }
        public string LicenseDocument { get; set; }
        public string DriverName { get; set; }
        public string DriverEmail { get; set; }
        public string Phone {  get; set; }
             public string Password { get; set; }
}

    public class DriverLoginDto
    {
      
        public string Username { get; set; }
       
        public string Password { get; set; }
    }
    public class OtpVerificationDto
    {
        public int OrderId { get; set; }
        public int DriverId { get; set; }
        public string EnteredOtp { get; set; }
    }
    public class DriverIdDto
    {
        public int DriverId { get; set; }
    }

  

    public class OrderDriverIdDto
    {
        public int OrderId { get; set; }
        public int DriverId { get; set; }
    }
    public class AcceptDeclineDto
    {
        public int OrderId { get; set; }
        public int DriverId { get; set; }
        public Boolean AcceptOrDecline {  get; set; }
    }
}
