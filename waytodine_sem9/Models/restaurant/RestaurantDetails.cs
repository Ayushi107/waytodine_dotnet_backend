using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace waytodine_sem9.Models.restaurant
{
    [Table("restaurant_details", Schema = "public")]
    public class RestaurantDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("restaurant_details_id")]
        public int RestaurantDetailsId { get; set; }

        [Column("current_offer_discount_rate")]
        public double? CurrentOfferDiscountRate { get; set; } // Nullable in case no discount is set

        [Column("opening_hours_weekdays")]
        public string OpeningHoursWeekdays { get; set; } // e.g., "Monday to Friday: 9:00 AM - 5:00 PM"

        [Column("opening_hours_weekends")]
        public string OpeningHoursWeekends { get; set; } // e.g., "Saturday to Sunday: 10:00 AM - 6:00 PM"

        [Column("specialities")]
        public string Specialities { get; set; } // Comma-separated list or JSON string of specialities

        [Column("mission")]
        public string Mission { get; set; }
        [Column("description")]
        public string Description { get; set; }

        [Column("banner_image")]
        public string BannerImage { get; set; } // URL or path to the banner image

        [JsonIgnore]
        [ForeignKey("Restaurant")]
        [Column("restaurant_id")]
        public int RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }

    }
}
