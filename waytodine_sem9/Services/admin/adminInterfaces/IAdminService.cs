﻿using waytodine_sem9.Controllers.admin;
using waytodine_sem9.Models.admin;

namespace waytodine_sem9.Services.admin.adminInterfaces
{
    public interface IAdminService
    {
        Task<string> LoginAsync(string username, string password);
        Task<Admin> RegisterAdminAsync(AdminRegisterDto registerDto);
        Task<Admin> UpdateAdminAsync(AdminUpdateDto adminUpdateDto);
        Task<string> ForgetPasswordAsync(ForgetPasswordDto forgetPasswordDto);
        Task<bool> VerifyOtpAsync(VerifyOtpDto verifyOtpDto);
        Task<bool> ChangePasswordAsync(ChangePasswordDto changePasswordDto);
        Task<Admin> GetProfileAsync();
        Task<bool> VerifyRestaurantUser(int res_id);
        Task<bool> VerifyDeliveryPerson(int del_id);
        Task<bool> VerifyRestaurantAsync(int resid);
        Task<bool> VerifyDriverAsync(int driverid);

    }
}
