﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace waytodine_sem9.Models.admin
{
    [Table("orders", Schema = "public")]
    public class Order
    {
      
       
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [Column("order_id")]
            public int OrderId { get; set; }

            [ForeignKey("CustomerId")]
            [Column("user_id")]
            public int CustomerId { get; set; }

            [ForeignKey("RestaurantId")]
            [Column("restaurant_id")]
            public int RestaurantId { get; set; }

            [ForeignKey("DeliveryPersonId")]
            [Column("delivery_person_id")]
            public int? DeliveryPersonId { get; set; }

          

            [Column("amount")]
            [Required]
            public decimal TotalAmount { get; set; }

            [Column("discount")]
            public decimal? Discount { get; set; }

            [Column("order_status")]
            public int OrderStatus { get; set; } = 1;  // 1 = placed, 2 = preparing, 3 = out for delivery, 4 = delivered

            [Column("payment_status")]
            public int PaymentStatus { get; set; } = 1;  // 1 = pending, 2 = completed, 3 = failed

        [Column("isAccept")]
        public Boolean IsAccept { get; set; } = false;

            [Column("created_at")]
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

            [Column("updated_at")]
            public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

            // Navigation Properties
            [ForeignKey("RestaurantId")]
            public Restaurant Restaurant { get; set; }

            [ForeignKey("CustomerId")]
            public User Customer { get; set; }

            [ForeignKey("DeliveryPersonId")]
            public DeliveryPerson DeliveryPerson { get; set; }


        }
    

}