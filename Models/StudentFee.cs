using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormMS.Web.Models
{
    [Table("Student_fees")]
    public class StudentFee
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Column("student_id")]
        public int studentId { get; set; }
        [ForeignKey("studentId")]
        public virtual Student? Student { get; set; }

        [Column("fee_id")]
        public int feeId { get; set; }
        [ForeignKey("feeId")]
        public virtual Fee? Fee { get; set; }

        [Column("amount")]
        public decimal amount { get; set; }

        [Column("status")]
        public string status { get; set; } = "Unpaid";

        [Column("due_date")]
        public DateTime dueDate { get; set; } = DateTime.Now.AddMonths(1);
    }
}

