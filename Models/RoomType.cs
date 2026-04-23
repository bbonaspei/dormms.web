using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormMS.Web.Models
{
    [Table("RoomTypes")]
    public class RoomType
    {
        [Key][Column("id")] public int id { get; set; }
        [Column("type_name")] public string typeName { get; set; } = string.Empty;
        public string? description { get; set; }
        public int capacity { get; set; }
        [Column("bed_count")] public int bedCount { get; set; }
        [Column("has_bathroom")] public bool hasBathroom { get; set; }
        [Column("has_air_conditioner")] public bool hasAirConditioner { get; set; }
        [Column("base_price_per_month")] public decimal basePrice { get; set; }
        [Column("fee_id")] public int? feeId { get; set; }
        [ForeignKey("feeId")] public virtual Fee? Fee { get; set; }
    }
}

