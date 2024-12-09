using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace waytodine_sem9.Models.admin
{
    [Table("cart", Schema = "public")]

    public class Cart
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("cart_id")]
        public int CartId { get; set; }

        [Column("user_id")]
        public int CustomerId { get; set; }


        [Column("quantity")]
        public int Quantity {  get; set; }

        [Column("status")]
        public int Status {  get; set; }

        [Column("total_price")]
        public int Total {  get; set; }

        [Column("item_id")]
        public int ItemId { get; set; }

        [Column("order_id")]
        public int OrderId {  get; set; }

        [Column("restaurant_id")]
        public int RestaurantId {  get; set; }
        

        [ForeignKey("CustomerId")]
        public User Customer { get; set; } // Navigation property


        [ForeignKey("ItemId")]
        public MenuItem Items { get; set; } // Navigation property

        [ForeignKey("OrderId")]
        public Order Orders { get; set; } // Navigation property

        [ForeignKey("RestaurantId")]
        public Restaurant Restaurant { get; set; } // Navigation property

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

         // Navigation property for cart items
    }
}
