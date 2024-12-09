using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace waytodine_sem9.Models.restaurant
{
    [Table("cart", Schema = "public")]
    public class Cart
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("cart_id")]
        public int CartId { get; set; }


        [Column("user_id")]
        public int userId { get; set; }


        [Column("quantity")]
        public int quantity { get; set; }


        [Column("status")]
        public int status { get; set; }


        [Column("total_price")]
        public int totalPrice {  get; set; }


        [Column("item_id")]
        public int itemId { get; set; }


        [Column("restaurant_id")]
        public int restaurantId { get; set; }


        [ForeignKey("userId")]
        public User Customer { get; set; } // Navigation property


        [ForeignKey("restaurantId")]
        public Restaurant Restaurant { get; set; }


        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    }
}
