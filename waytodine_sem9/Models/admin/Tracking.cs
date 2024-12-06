using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace waytodine_sem9.Models.admin
{
    public class Tracking
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrackingId { get; set; }
        public int DeliveryPersonId { get; set; }
        public decimal CurrentLatitude { get; set; }
        public decimal CurrentLongitude { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;


       

        [ForeignKey("DeliveryPersonId")]
        public DeliveryPerson DeliveryPerson { get; set; } // Navigation property



    }


}
