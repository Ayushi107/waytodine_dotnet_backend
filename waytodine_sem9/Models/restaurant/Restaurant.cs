using Microsoft.Extensions.Configuration.UserSecrets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace waytodine_sem9.Models.restaurant
{
    [Table("restaurants", Schema = "public")]
    public class Restaurant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("restaurant_id")]
        public int RestaurantId { get; set; }

        [Required]
        [StringLength(255)]
        [Column("name")] // Match the column name in the database
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        [EmailAddress]
        [Column("email")]
        public string Email { get; set; }

        [StringLength(255)]
        [Column("password")]
        public string Password { get; set; }

        [Required]
        [StringLength(15)]
        [Column("phone_number")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(255)]
        [Column("location")]
        public string Location { get; set; }

        [Required]
        [StringLength(100)]
        [Column("city")]
        public string City { get; set; }

        [Required]
        [StringLength(100)]
        [Column("country")]
        public string Country { get; set; }

        [Column("status")]
        public int Status { get; set; } = 0;

        [StringLength(255)]
        [Column("restaurant_document")]
        public string RestaurantDocument { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")] 
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<MenuItem> MenuItems { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<RestaurantDetails> RestaurantDetails { get; set; }



    }



}
