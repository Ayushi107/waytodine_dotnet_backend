using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace waytodine_sem9.Models.admin
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewId { get; set; }
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public int? DeliveryPersonId { get; set; }
        public int Rating { get; set; } // Ensure validation (1-5) in the service layer
        public string ReviewText { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        

        [ForeignKey("CustomerId")]
        public User Customer { get; set; } // Navigation property

        [ForeignKey("RestaurantId")]
        public Restaurant Restaurant { get; set; } // Navigation property

        [ForeignKey("DeliveryPersonId")]
        public DeliveryPerson DeliveryPerson { get; set; } // Optional navigation property




    }
}
