using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormMS.Web.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string passwordHash { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? phone { get; set; }
        public string? profilePicture { get; set; }
        public bool isActive { get; set; } = true;
        public DateTime? lastLogin { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;
    }
}