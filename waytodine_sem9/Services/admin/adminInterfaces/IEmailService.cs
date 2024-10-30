namespace waytodine_sem9.Services.admin.adminInterfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
