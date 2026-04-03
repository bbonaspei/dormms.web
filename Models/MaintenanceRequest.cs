using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormMS.Web.Models
{
    [Table("Maintenance_requests")]
    public class MaintenanceRequest
    {
        [Key]
        public int id { get; set; }
        public string requestNumber { get; set; } = "";

        public int studentId { get; set; }
        [ForeignKey("studentId")]
        public virtual Student? Student { get; set; }

        public int roomId { get; set; }
        [ForeignKey("roomId")]
        public virtual Room? Room { get; set; }

        public string issueCategory { get; set; } = "General"; // Plumbing, Electrical, etc.
        public string description { get; set; } = "";
        public string priority { get; set; } = "Medium"; // Low, Medium, High
        public string status { get; set; } = "Pending"; // Pending, In Progress, Completed

        public int? assignedTo { get; set; } // Personel ID (User tablosuna bağlı)
        [ForeignKey("assignedTo")]
        public virtual User? Staff { get; set; }

        public DateTime requestDate { get; set; } = DateTime.Now;
        public DateTime? completedDate { get; set; }
        public int? studentRating { get; set; }
    }
}