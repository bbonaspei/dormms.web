using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormMS.Web.Models
{
    [Table("Fees")]
    public class Fee
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Required]
        [Column("fee_name")]
        public string feeName { get; set; } = string.Empty;

        [Column("fee_category")]
        public string? feeCategory { get; set; }

        [Column("amount")]
        public decimal amount { get; set; }

        [Column("is_recurring")]
        public bool isRecurring { get; set; } = true;

        [Column("recurrence_interval")]
        public string? recurrenceInterval { get; set; }

        [Column("description")]
        public string? description { get; set; }

        public virtual ICollection<RoomType>? RoomTypes { get; set; }
    }
}

