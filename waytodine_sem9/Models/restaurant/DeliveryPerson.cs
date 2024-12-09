using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace waytodine_sem9.Models.restaurant
{

    [Table("delivery_person", Schema = "public")]
    public class DeliveryPerson
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("delivery_person_id")]
        public int DeliveryPersonId { get; set; }

        [Required]
        [Column("vehicle_type")]
        public string VehicleType { get; set; }

        [Required]
        [Column("vehicle_number")]
        public string VehicleNumber { get; set; }

        [Required]
        [Column("driving_license_number")]
        public string DrivingLicenseNumber { get; set; }

        [Column("is_available")]
        public Boolean IsAvailable { get; set; }=false;

        [Required]
        [Column("license_document")]
        public string LicenseDocument { get; set; }

        [Column("driver_name")]
        public string DriverName { get; set; }

        [Column("driver_email")]
        public string DriverEmail { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("status")]
        public int status { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public ICollection<Order> Orders { get; set; }
    
    }
}
