using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormMS.Web.Models
{
    [Table("Notifications")]
    public class Notification
    {
        [Key]
        public int id { get; set; }

        public int userId { get; set; } // Bildirimin kime gideceği
        [ForeignKey("userId")]
        public virtual User? User { get; set; }

        public string title { get; set; } = "";
        public string message { get; set; } = "";
        public string type { get; set; } = "Info"; // Info, Success, Warning, Error
        public bool isRead { get; set; } = false;
        public DateTime createdAt { get; set; } = DateTime.Now;
    }
}