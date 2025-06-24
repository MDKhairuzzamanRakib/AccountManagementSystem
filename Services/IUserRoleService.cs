using AccountManagementSystem.Data.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace AccountManagementSystem.Services
{
    public interface IUserRoleService
    {
        Task<List<UserRoleViewModel>> GetAllUsersWithRolesAsync();
        Task<List<IdentityRole>> GetAllRolesAsync();
        Task<IdentityResult> AddUserToRoleAsync(string userId, string roleName);
        Task<IdentityResult> RemoveUserFromRoleAsync(string userId, string roleName);
        Task<bool> CheckUserPermissionAsync(string userId, string permission);
    }
}
