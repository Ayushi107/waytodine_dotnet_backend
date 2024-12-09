using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using waytodine_sem9.Data;
using waytodine_sem9.Models.admin;
using waytodine_sem9.Repositories.admin.adminInterfaces;
using static waytodine_sem9.Controllers.admin.CategoryController;

namespace waytodine_sem9.Repositories.admin.adminClasses
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly Cloudinary _cloudinary;

      
      

        public CategoryRepository(ApplicationDbContext context, Cloudinary cloudinary)
        {
            _context = context;
            _cloudinary = cloudinary;
        }

        public async Task<object> GetAllCategories(int pageNumber, int pageSize)
        {
            var totalRecords = await _context.Categories.CountAsync();
            var category = await _context.Categories
                .Include(c => c.MenuItems)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize),
                Data = category
            };
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Categories.FindAsync(id);
        }
        public string SaveProfilePicFromBase64(string base64Image, string originalFileName)
        {


            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64Image);

                // Use a MemoryStream to upload the image to Cloudinary
                using (var stream = new MemoryStream(imageBytes))
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(originalFileName, stream),
                        PublicId = $"{originalFileName}_{DateTime.UtcNow.Ticks}", // Optional: Custom Public ID for Cloudinary
                        Overwrite = true // If you want to overwrite existing images with the same PublicId
                    };

                    var uploadResult = _cloudinary.Upload(uploadParams);

                    if (uploadResult?.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        // Return the secure URL of the uploaded image from Cloudinary
                        return uploadResult.SecureUrl.ToString(); // This is the URL for use in your database
                    }
                    else
                    {
                        throw new Exception("Failed to upload image to Cloudinary.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle Cloudinary upload exceptions
                Console.WriteLine($"Error uploading image to Cloudinary: {ex.Message}");
                throw new Exception("Failed to upload image to Cloudinary.");
            }

            // Define the directory where the image will be saved
            //string directory = "F:\\MscIT\\sem9\\Project\\Backend\\waytodine_sem9\\waytodine_sem9\\wwwroot\\uploads";

            //// Create the directory if it doesn't exist
            //if (!Directory.Exists(directory))
            //{
            //    Directory.CreateDirectory(directory); // Create directory and any necessary subdirectories
            //}

            //string fileName = $"{originalFileName}_{DateTime.UtcNow.Ticks}.jpg"; // You can change the extension based on the image type
            //string filePath = Path.Combine(directory, fileName);
            //string relativeImagePath = Path.Combine("uploads", fileName);

            // Decode the Base64 string and save it as a file
            //try
            //{
            //    byte[] imageBytes = Convert.FromBase64String(base64Image);
            //    File.WriteAllBytes(filePath, imageBytes); // Save the byte array to the file
            //}
            //catch (Exception e)
            //{
            //    // Handle any exceptions (e.g., invalid Base64 string)
            //    Console.WriteLine($"Error saving profile picture: {e.Message}");
            //    throw new Exception("Failed to save the profile picture.");
            //}

            //// Return the file name for database storage (or any other necessary data)
            //return relativeImagePath;
        }
        public async Task<bool> CreateCategory(CategoryDto category)
        {
            string imageFileName = SaveProfilePicFromBase64(category.Image, category.Name);
           var cat = new Category
           {
               CategoryName = category.Name,
               Description = category.Description,
               status = category.Status,
               categoryImage = imageFileName // Save the image file name to the database
           };

            _context.Categories.Add(cat);
            _context.SaveChanges();

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateCategory(Category category)
        {
            // Find the existing category from the database using the category ID.
            var existingCategory = await _context.Categories
                                                .FirstOrDefaultAsync(c => c.CategoryId == category.CategoryId);

            if (existingCategory == null)
            {
                return false; // Category not found
            }

            // Update only the fields that are not null or empty in the incoming category
            if (!string.IsNullOrEmpty(category.CategoryName))
            {
                existingCategory.CategoryName = category.CategoryName;
            }

            if (!string.IsNullOrEmpty(category.Description))
            {
                existingCategory.Description = category.Description;
            }

            if (!string.IsNullOrEmpty(category.categoryImage))
            {
                existingCategory.categoryImage = category.categoryImage; // Update the image only if it's provided
            }

            if (category.status != null)
            {
                existingCategory.status = category.status; // Update the status only if it's provided
            }

            // Save the changes to the database
            _context.Categories.Update(existingCategory);
            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<bool> DeleteCategory(int id)
        {
            var category = await GetCategoryById(id);
            if (category == null) return false;

            _context.Categories.Remove(category);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
