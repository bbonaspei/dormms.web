using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormMS.Web.Models
{
    [Table("Notifications")]
    public class Notification
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Column("user_id")]
        public int userId { get; set; }
        [ForeignKey("userId")]
        public virtual User? User { get; set; }

        [Column("title")]
        public string title { get; set; } = string.Empty;

        [Column("message")]
        public string message { get; set; } = string.Empty;

        [Column("type")] // Success, Warning, Info, Error
        public string type { get; set; } = "Info";

        [Column("is_read")]
        public bool isRead { get; set; } = false;

        [Column("target_url")] // Tıklanınca gidilecek adres (Örn: /Payments/Index)
        public string? targetUrl { get; set; }

        [Column("created_at")]
        public DateTime createdAt { get; set; } = DateTime.Now;
    }
}