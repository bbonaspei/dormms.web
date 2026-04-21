using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormMS.Web.Models
{
    public class Role
    {
        [Key]
        [Column("id")] // Veritabanındaki küçük 'id' sütununa bağlan
        public int Id { get; set; }
        [Required, StringLength(50)]

        [Column("roleName")]
        public string RoleName { get; set; } = string.Empty;

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        // Navigation properties
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}