using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormMS.Web.Models
{
    [Table("RoomTypes")]
    public class RoomType
    {
        [Key]
        public int id { get; set; }

        public string typeName { get; set; } // type_name değil

        public string? description { get; set; }

        public int capacity { get; set; }

        public int bedCount { get; set; } // bed_count değil

        public bool hasBathroom { get; set; } // has_bathroom değil

        public decimal basePrice { get; set; } // base_price değil
    }
}