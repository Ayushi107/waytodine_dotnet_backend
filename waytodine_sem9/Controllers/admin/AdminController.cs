using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using waytodine_sem9.Services.admin.adminInterfaces;

namespace waytodine_sem9.Controllers.admin
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController:ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
     

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AdminLoginDto adminLoginDto)
        {
            var token = await _adminService.LoginAsync(adminLoginDto.Username, adminLoginDto.Password);
            if (token == null)
            {
                return Unauthorized("Invalid username or password.");
            }
            return Ok(new { Token = token });
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AdminRegisterDto registerDto)
        {
            if (registerDto == null || string.IsNullOrEmpty(registerDto.Username) || string.IsNullOrEmpty(registerDto.Password))
            {
                return BadRequest("Username and password are required.");
            }

            var admin = await _adminService.RegisterAdminAsync(registerDto);
            if (admin == null)
            {
                return StatusCode(500, "An error occurred while creating the admin.");
            }

            return Ok(new { Message = "Admin registered successfully.", AdminId = admin.Id });
        }


        [HttpPut("update-admin")]
        public async Task<IActionResult> UpdateAdmin([FromBody] AdminUpdateDto adminUpdateDto)
        {
            if (adminUpdateDto == null || adminUpdateDto.Id <= 0)
            {
                return BadRequest("Invalid admin data.");
            }

            var updatedAdmin = await _adminService.UpdateAdminAsync(adminUpdateDto);
            if (updatedAdmin == null)
            {
                return NotFound("Admin not found.");
            }

            return Ok(new { Message = "Admin updated successfully.", Admin = updatedAdmin });
        }


        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordDto forgetPasswordDto)
        {
            var result = await _adminService.ForgetPasswordAsync(forgetPasswordDto);
            return result == null ? NotFound("Admin not found.") : Ok(result);
        }


        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpDto verifyOtpDto)
        {
            var isVerified = await _adminService.VerifyOtpAsync(verifyOtpDto);
            return isVerified ? Ok("OTP verified successfully.") : BadRequest("Invalid OTP.");
        }


        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
           
            var isChanged = await _adminService.ChangePasswordAsync(changePasswordDto);
            return isChanged ? Ok("Password changed successfully.") : BadRequest("Failed to change password.");
        }


        [HttpPost("get-profile")]
        public async Task<IActionResult> GetProfile(GetProfileDto getprofiledto)
        {
            var username = getprofiledto.Username;
            var adminProfile = await _adminService.GetProfileAsync(username);
            return adminProfile == null ? NotFound("Admin not found.") : Ok(adminProfile);
        }


    }







    public class AdminLoginDto
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }

    public class AdminRegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string Gender { get; set; }
    }

    public class AdminUpdateDto
    {
        public int Id { get; set; } // Admin ID
        public string Username { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string Gender { get; set; }
    }

    public class GetProfileDto
    {
        public string Username { get; set; }
    }

    public class ForgetPasswordDto
    {
        public string Email { get; set; }
    }

    public class VerifyOtpDto
    {
        public string Email { get; set; }
        public string Otp { get; set; }
    }

    public class ChangePasswordDto
    {
        public string NewPassword { get; set; }
        public string UserName { get; set; }
    }

   
}
