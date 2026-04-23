using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormMS.Web.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("username")]
        public string username { get; set; } = string.Empty;

        [Column("email")]
        public string email { get; set; } = string.Empty;

        [Column("passwordHash")]
        public string passwordHash { get; set; } = string.Empty;

        [Column("firstName")]
        public string? firstName { get; set; }

        [Column("lastName")]
        public string? lastName { get; set; }

        [Column("phone")]
        public string? phone { get; set; }

        [Column("profilePicture")]
        public string? profilePicture { get; set; }

        [Column("isActive")]
        public bool isActive { get; set; } = true;

        [Column("lastLogin")]
        public DateTime? lastLogin { get; set; }

        [Column("createdAt")]
        public DateTime createdAt { get; set; } = DateTime.Now;

        [Column("updatedAt")]
        public DateTime updatedAt { get; set; } = DateTime.Now;

        public virtual ICollection<UserRole>? UserRoles { get; set; }
        public virtual ICollection<Notification>? Notifications { get; set; }
    }
}

