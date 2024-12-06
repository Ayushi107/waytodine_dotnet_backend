using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace waytodine_sem9.Models.admin
{
    public class DeliveryPerson
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeliveryPersonId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string VehicleType { get; set; }

        [Required]
        public string VehicleNumber { get; set; }

        [Required]
        public string DrivingLicenseNumber { get; set; }

        public Boolean IsAvailable { get; set; }=false;

        [Required]
        public string LicenseDocument { get; set; }

        public string DriverName { get; set; }
        public string DriverEmail { get; set; } 
        public string Phone { get; set; }
        public string Password { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [ForeignKey("UserId")]
        public User User { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Tracking> Trackings { get; set; }

        // remove all this user orders review trackings
    }
}
