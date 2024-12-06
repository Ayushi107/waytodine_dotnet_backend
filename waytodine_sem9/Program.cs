using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using waytodine_sem9.Data;
using waytodine_sem9.Repositories.admin.adminClasses;
using waytodine_sem9.Repositories.admin.adminInterfaces;
using waytodine_sem9.Repositories.driver.driverClasses;
using waytodine_sem9.Repositories.driver.driverInterfaces;
using waytodine_sem9.Services.admin.adminClasses;
using waytodine_sem9.Services.admin.adminInterfaces;
using waytodine_sem9.Services.driver.driverClasses;
using waytodine_sem9.Services.driver.driverInterfaces;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy("Allow", policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:3000")
                     .AllowAnyMethod()
                     .AllowAnyHeader()
                     .AllowCredentials();

    });
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IListingRepository, ListingRepository>();
builder.Services.AddScoped<IListingService, ListingService>();
builder.Services.AddScoped<ISearchingRepository, SearchingRepository>();
builder.Services.AddScoped<ISearchingService, SearchingService>();
builder.Services.AddScoped<IdriverRepository, driverRepository>();
builder.Services.AddScoped<IdriverService, driverService>();





// Add services to the container.
builder.Services.AddMemoryCache();

builder.Services.AddControllers()
      .AddJsonOptions(options =>
       {
           options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
           options.JsonSerializerOptions.WriteIndented = true;
       });


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

// Ensure CORS is set up before authorization
app.UseCors("Allow");

app.UseAuthorization();

app.MapControllers();

SeedDatabase(app);

app.Run();


void SeedDatabase(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated(); // Creates the database if it doesn't exist

            // Call the seeding method
            //SeedData(context);
        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., log the error)
            throw;
        }
    }
}
