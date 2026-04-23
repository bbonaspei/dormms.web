using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormMS.Web.Models
{
    [Table("Allocations")]
    public class Allocation
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Column("student_id")]
        public int studentId { get; set; }
        [ForeignKey("studentId")]
        public virtual Student? Student { get; set; }

        [Column("room_id")]
        public int roomId { get; set; }
        [ForeignKey("roomId")]
        public virtual Room? Room { get; set; }

        [Column("start_date")]
        public DateTime startDate { get; set; }

        [Column("end_date")]
        public DateTime? endDate { get; set; }

        [Column("actual_check_in")]
        public DateTime? actualCheckIn { get; set; }

        [Column("actual_check_out")]
        public DateTime? actualCheckOut { get; set; }

        [Column("is_current")]
        public bool isCurrent { get; set; } = true;

        [Column("status")]
        public string status { get; set; } = "Confirmed";

        [Column("security_deposit")]
        public decimal securityDeposit { get; set; }

        [Column("key_card_number")]
        public string? keyCardNumber { get; set; }

        [Column("created_at")]
        public DateTime createdAt { get; set; } = DateTime.Now;

        [Column("created_by")]
        public int? createdBy { get; set; }
    }
}

