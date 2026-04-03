using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormMS.Web.Models
{
    [Table("Student_fees")]
    public class StudentFee
    {
        [Key]
        public int id { get; set; }

        public int studentId { get; set; }
        [ForeignKey("studentId")]
        public virtual Student? Student { get; set; }

        public int feeId { get; set; }
        [ForeignKey("feeId")]
        public virtual Fee? Fee { get; set; }

        public decimal amount { get; set; }

        public string status { get; set; } = "Unpaid";
    }
}