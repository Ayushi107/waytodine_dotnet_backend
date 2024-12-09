using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace waytodine_sem9.Models.restaurant
{
    [Table("feedback", Schema = "public")]
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("feedback_id")]
        public int FeedbackId { get; set; }

        [ForeignKey("Restaurant")]
        [Column("restaurant_id")]
        public int RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }

        [ForeignKey("Customer")]
        [Column("customer_id")]
        public int userId { get; set; }

        public  User Customer { get; set; }

        [Column("rating")]
        public int Rating { get; set; }

        [Column("review")]
        public string Review { get; set; }


    }
}
