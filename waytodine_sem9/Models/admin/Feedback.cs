using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace waytodine_sem9.Models.admin
{
    [Table("feedback", Schema = "public")]
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeedbackId { get; set; }

        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public  User Customer { get; set; }

        public int Rating { get; set; }

        public string Review { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
