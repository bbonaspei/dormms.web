using System.ComponentModel.DataAnnotations.Schema;

namespace DormMS.Web.Models
{
    [Table("User_roles")]
    public class UserRole
    {
        [Column("userId")]
        public int UserId { get; set; }

        [Column("roleId")]
        public int RoleId { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role? Role { get; set; }

        [Column("assignedAt")]
        public DateTime AssignedAt { get; set; } = DateTime.Now;
    }
}

