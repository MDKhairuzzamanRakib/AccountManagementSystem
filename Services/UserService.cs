using AccountManagementSystem.Data;
using AccountManagementSystem.Models;
using System.Threading.Tasks;

namespace AccountManagementSystem.Services
{
    public interface IUserService
    {
        Task<UserPermission> GetUserPermissions(string userId);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<UserPermission> GetUserPermissions(string userId) => _userRepository.GetUserPermissions(userId);
    }
}