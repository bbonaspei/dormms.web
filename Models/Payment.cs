using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormMS.Web.Models
{
    [Table("Payments")]
    public class Payment
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Column("payment_reference")]
        public string paymentReference { get; set; } = string.Empty;

        [Column("student_id")]
        public int studentId { get; set; }
        [ForeignKey("studentId")]
        public virtual Student? Student { get; set; }

        [Column("amount")]
        public decimal amount { get; set; }

        [Column("payment_date")]
        public DateTime paymentDate { get; set; } = DateTime.Now;

        [Column("payment_method")]
        public string paymentMethod { get; set; } = "Credit Card";

        [Column("transaction_id")]
        public string? transactionId { get; set; }

        [Column("notes")]
        public string? notes { get; set; }

        [Column("received_by")]
        public int? receivedBy { get; set; }

        [Column("status")]
        public string status { get; set; } = "Success";
    }
}

