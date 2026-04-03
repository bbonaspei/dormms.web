using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormMS.Web.Models
{
    [Table("Payments")]
    public class Payment
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string paymentReference { get; set; } = string.Empty;

        public int studentId { get; set; }

        [ForeignKey("studentId")]
        public virtual Student? Student { get; set; }

        public decimal amount { get; set; }

        public DateTime paymentDate { get; set; } = DateTime.Now;

        public string paymentMethod { get; set; } = "Cash";

        public string status { get; set; } = "Success";
    }
}