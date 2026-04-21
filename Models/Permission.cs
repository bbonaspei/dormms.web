using System.ComponentModel.DataAnnotations;

namespace DormMS.Web.Models
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string PermissionName { get; set; } = string.Empty; // Örn: "ManageRooms", "ViewPayments"
        public string Module { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}