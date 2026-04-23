using System.ComponentModel.DataAnnotations.Schema;

namespace DormMS.Web.Models
{
    [Table("Role_permissions")]
    public class RolePermission
    {
        [Column("roleId")]
        public int RoleId { get; set; }

        [Column("permissionId")]
        public int PermissionId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; } = null!;

        [ForeignKey("PermissionId")]
        public virtual Permission Permission { get; set; } = null!;
    }
}

