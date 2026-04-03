using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormMS.Web.Models
{
    [Table("Allocations")]
    public class Allocation
    {
        [Key]
        public int id { get; set; }

        public int studentId { get; set; }
        [ForeignKey("studentId")]
        public virtual Student? Student { get; set; }

        public int roomId { get; set; }
        [ForeignKey("roomId")]
        public virtual Room? Room { get; set; }

        public DateTime startDate { get; set; }
        public DateTime? endDate { get; set; }
        public DateTime? actualCheckIn { get; set; }
        public bool isCurrent { get; set; } = true;
        public string status { get; set; } = "Confirmed";

        public decimal securityDeposit { get; set; }
    }
}