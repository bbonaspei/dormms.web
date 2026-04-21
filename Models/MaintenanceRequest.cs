using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormMS.Web.Models
{
    [Table("Maintenance_requests")]
    public class MaintenanceRequest
    {
        // YENİ EKLENDİ: Sınıf oluşurken durum KESİNLİKLE Pending başlar
        public MaintenanceRequest()
        {
            status = "Pending";
        }

        [Key]
        [Column("id")]
        public int id { get; set; }

        [Column("request_number")]
        public string? requestNumber { get; set; }

        [Column("student_id")]
        public int? studentId { get; set; }

        [ForeignKey("studentId")]
        public virtual Student? Student { get; set; }

        [Column("room_id")]
        public int? roomId { get; set; }

        [ForeignKey("roomId")]
        public virtual Room? Room { get; set; }

        [Column("issue_category")]
        public string? issueCategory { get; set; }

        [Column("description")]
        public string? description { get; set; }

        [Column("priority")]
        public string? priority { get; set; }

        [Column("status")]
        public string? status { get; set; }

        [Column("assigned_to")]
        public int? assignedTo { get; set; }

        [ForeignKey("assignedTo")]
        public virtual User? Staff { get; set; }

        [Column("request_date")]
        public DateTime? requestDate { get; set; }

        [Column("completed_date")]
        public DateTime? completedDate { get; set; }

        [Column("student_rating")]
        public int? studentRating { get; set; }

        [Column("student_feedback")]
        public string? studentFeedback { get; set; }
    }
}