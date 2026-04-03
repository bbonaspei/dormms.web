using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormMS.Web.Models
{
    [Table("Audit_logs")]
    public class AuditLog
    {
        [Key]
        public int id { get; set; }

        public int? userId { get; set; } // İşlemi yapan kişi
        [ForeignKey("userId")]
        public virtual User? User { get; set; }

        public string action { get; set; } = ""; // "CREATE", "UPDATE", "DELETE"
        public string entityType { get; set; } = ""; // "Room", "Student", "Payment"
        public int entityId { get; set; } // Hangi ID'li kayıt değişti?

        public string? oldValues { get; set; } // Değişmeden önceki hali
        public string? newValues { get; set; } // Değiştikten sonraki hali

        public string? ipAddress { get; set; }
        public string? userAgent { get; set; } // Hangi tarayıcıdan yaptı?
        public DateTime createdAt { get; set; } = DateTime.Now;
    }
}