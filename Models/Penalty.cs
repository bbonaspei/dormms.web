using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormMS.Web.Models
{
    [Table("Penalties")]
    public class Penalty
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Column("student_id")]
        public int studentId { get; set; }
        [ForeignKey("studentId")]
        public virtual Student? Student { get; set; }

        [Column("penalty_type")]
        public string penaltyType { get; set; } = "Late Payment";

        [Column("amount")]
        public decimal amount { get; set; }

        [Column("applied_date")]
        public DateTime appliedDate { get; set; } = DateTime.Now;

        [Column("reason")]
        public string? reason { get; set; }

        [Column("status")]
        public string status { get; set; } = "Pending"; // Pending, Paid, Waived
    }
}