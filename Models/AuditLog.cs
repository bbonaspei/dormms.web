using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormMS.Web.Models
{
    [Table("Audit_logs")]
    public class AuditLog
    {
        [Key]
        public int id { get; set; }

        public int? userId { get; set; }
        [ForeignKey("userId")]
        public virtual User? User { get; set; }

        public string action { get; set; } = "";
        public string entityType { get; set; } = "";
        public int entityId { get; set; }

        public string? oldValues { get; set; }
        public string? newValues { get; set; }

        public string? ipAddress { get; set; }
        public string? userAgent { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;
    }
}

