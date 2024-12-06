using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace waytodine_sem9.Models.admin
{
    [Table("menu_item", Schema = "public")]
    public class MenuItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("item_id")]
        public int ItemId { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Column("description", TypeName = "varchar(500)")]
        public string Description { get; set; }

        [Required]
        [Column("price")]
        public int Price { get; set; } // Integer to match the Spring model

        [Column("item_image")]
        public string ItemImage { get; set; } // URL or path for the item image

        [Required]
        [Column("is_veg")]
        public int IsVeg { get; set; } // 1 for veg, 2 for non-veg

        [Required]
        [Column("status", TypeName = "int")]
        public int Status { get; set; } = 1; // 1 for active, 0 for inactive

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [Required]
        [ForeignKey("Restaurant")]
        [Column("restaurant_id")]
        public int RestaurantId { get; set; }

        public  Restaurant Restaurant { get; set; }

        [Required]
        [ForeignKey("Category")]
        [Column("category_id")]
        public int CategoryId { get; set; }

        public  Category Category { get; set; }


    }
}
