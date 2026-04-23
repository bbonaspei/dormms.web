using DormMS.Web.Data;
using DormMS.Web.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DormMS.Web.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly ApplicationDbContext _context;

        public PermissionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> HasPermissionAsync(int userId, string permissionName)
        {

            var permissions = await (from ur in _context.UserRoles
                                     join rp in _context.RolePermissions on ur.RoleId equals rp.RoleId
                                     join p in _context.Permissions on rp.PermissionId equals p.Id
                                     where ur.UserId == userId && p.PermissionName == permissionName
                                     select p.PermissionName).ToListAsync();

            return permissions.Contains(permissionName);
        }
    }
}

