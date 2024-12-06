using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using waytodine_sem9.Repositories.admin.adminInterfaces;
using waytodine_sem9.Services.admin.adminInterfaces;
using Microsoft.IdentityModel.Tokens;
using waytodine_sem9.Controllers.admin;
using waytodine_sem9.Models.admin;
using static System.Net.WebRequestMethods;
using Microsoft.Extensions.Caching.Memory;


namespace waytodine_sem9.Services.admin.adminClasses
{
    public class AdminService:IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly IMemoryCache _memoryCache;
        private string _otp;

        public AdminService(IAdminRepository adminRepository, IConfiguration configuration, IEmailService emailService, IMemoryCache memoryCache)
        {
            _adminRepository = adminRepository;
            _configuration = configuration;
            _emailService = emailService;
            _memoryCache = memoryCache;
        }
        public async Task<string> LoginAsync(string username, string password)
        {
            var admin = await _adminRepository.GetByUsernameAndPasswordAsync(username, password);
            if (admin == null)
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
                    new Claim(ClaimTypes.Name, admin.Username),
                    new Claim(ClaimTypes.Role, "Admin")
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


        public async Task<Admin> RegisterAdminAsync(AdminRegisterDto registerDto)
        {
            // Hash the password
            //var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            // Create a new admin object
            var admin = new Admin
            {
                Username = registerDto.Username,
                Password = registerDto.Password,
                Email = registerDto.Email,
                Image = registerDto.Image,
                Gender = registerDto.Gender
            };

            // Save the admin to the database
            return await _adminRepository.AddAdmin(admin);
        }


        public async Task<Admin> UpdateAdminAsync(AdminUpdateDto adminUpdateDto)
        {
            var admin = new Admin
            {
                Id = adminUpdateDto.Id,
                Username = adminUpdateDto.Username,
                Email = adminUpdateDto.Email,
                Image = adminUpdateDto.Image,
                Gender = adminUpdateDto.Gender
            };

            return await _adminRepository.UpdateAdmin(admin);
        }


        public async Task<string> ForgetPasswordAsync(ForgetPasswordDto forgetPasswordDto)
        {
            var admin = await _adminRepository.GetAdminByEmail(forgetPasswordDto.Email);
            if (admin == null)
            {
                return null;
            }

            _otp = GenerateOtp();
            Console.WriteLine(_otp);
            _memoryCache.Set($"Otp_{forgetPasswordDto.Email}", _otp, TimeSpan.FromMinutes(10));

            await _emailService.SendEmailAsync(forgetPasswordDto.Email, "Your OTP Code", $"Your OTP is: {_otp}");
            return "OTP sent to your email.";
        }

        public async Task<bool> VerifyOtpAsync(VerifyOtpDto verifyOtpDto)
        {
            if (_memoryCache.TryGetValue($"Otp_{verifyOtpDto.Email}", out string cachedOtp) && cachedOtp == verifyOtpDto.Otp)
            {
                // Remove OTP after successful verification to avoid reuse
                _memoryCache.Remove($"Otp_{verifyOtpDto.Email}");
                return true;
            }
            return false;

            //Console.WriteLine(_otp);

            //return _otp == verifyOtpDto.Otp;
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
        {
            //var hashedPassword = HashPassword(changePasswordDto.NewPassword);
            return await _adminRepository.UpdatePassword(changePasswordDto.UserName, changePasswordDto.NewPassword);
        }

        public async Task<Admin> GetProfileAsync()
        {
            return await _adminRepository.GetAdmin();
        }

        private string GenerateOtp()
        {
            return new Random().Next(100000, 999999).ToString();
        }

        public async Task<bool> VerifyRestaurantUser(int res_id)
        {
            var restaurantUSer = await _adminRepository.GetRestaurantById(res_id);
            if (restaurantUSer == null)
            {
                return false;
            }
            //await _emailService.SendEmailAsync(restaurantUSer.Id, "You can now register to the dashboard");
            return true;
        }

        public async Task<bool> VerifyDeliveryPerson(int del_id)
        {
            var deliveryPerson = await _adminRepository.GetDeliveryPersonById(del_id);
            if(deliveryPerson == null)
            {
                return false;
            }
            //await _emailService.SendEmailAsync(restaurantUSer.Id, "You can now register to the dashboard");
            return true;
        }


        public async Task<bool> VerifyRestaurantAsync(int resid)
        {
            var email = await _adminRepository.VerifyRestaurant(resid);
            if (email == null)
            {
                return false;
            }
            else
            {
                string loginUrl = "https://localhost:3000";
                const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                Random random = new Random();

                // Generate a random number
                string numericPart = random.Next(100000, 999999).ToString();

                // Add random characters to the code
                string charPart = new string(Enumerable.Repeat(validChars, 4)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
                string password = numericPart + charPart;

                string emailMessage = $@"
                {email}
                <h3>Welcome to Our Platform!</h3>
                <p>Your account has been verified and you can now log in.</p>
                <p><b>Login Link:</b> <a href='{loginUrl}'>{loginUrl}</a></p>
                <p><b>Your Temporary Password:</b> {password}</p>
                <p>We recommend changing your password after logging in.</p>
                ";

                await _emailService.SendEmailAsync(email, "Verification update", emailMessage);
                return true;
            }
        }


        public async Task<bool> VerifyDriverAsync(int driverid)
        {
            var email = await _adminRepository.VerifyDriver(driverid);
            if (email == null)
            {
                return false;
            }
            else
            {
                string loginUrl = "https://localhost:3000";
                const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                Random random = new Random();

                // Generate a random number
                string numericPart = random.Next(100000, 999999).ToString();

                // Add random characters to the code
                string charPart = new string(Enumerable.Repeat(validChars, 4)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
                string password = numericPart + charPart;

                string emailMessage = $@"
                {email}
                <h3>Welcome to Our Platform!</h3>
                <p>Your account has been verified and you can now log in.</p>
                <p><b>Login Link:</b> <a href='{loginUrl}'>{loginUrl}</a></p>
                <p><b>Your Temporary Password:</b> {password}</p>
                <p>We recommend changing your password after logging in.</p>
                ";

                await _emailService.SendEmailAsync(email, "Verification update", emailMessage);
                return true;
            }
        }

    }
}
