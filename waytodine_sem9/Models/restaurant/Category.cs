using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace waytodine_sem9.Models.restaurant
{
    [Table("categories", Schema = "public")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("category_id")]
        public int CategoryId { get; set; }


        [Required]
        [Column("name")]
        public string CategoryName { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("status")]
        public int status { get; set; } = 1;

        [Column("category_image")]
        public string categoryImage {  get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties

        [JsonIgnore]
        public ICollection<MenuItem> MenuItems { get; set; }
    }
}
