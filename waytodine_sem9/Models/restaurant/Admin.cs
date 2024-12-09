using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace waytodine_sem9.Models.restaurant
{
    [Table("admin", Schema = "public")]
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Primary Key, auto-increment

        [Required]
        [StringLength(50)]
        public string Username { get; set; } // Username with max length of 50 characters

        [Required]
        [StringLength(100)]
        public string Password { get; set; } // Hashed password with max length of 100 characters

        [StringLength(255)]
        public string? Image { get; set; } // Image URL, optional field

        [Required]
        [EmailAddress]
        public string Email { get; set; } // Email with proper validation

        [StringLength(10)]
        public string? Gender { get; set; } // Optional gender field

    }
}
