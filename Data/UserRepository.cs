using AccountManagementSystem.Models;
using Dapper;
using System.Data;
using System.Threading.Tasks;

namespace AccountManagementSystem.Data
{
    public interface IUserRepository
    {
        Task<UserPermission> GetUserPermissions(string userId);
    }

    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;

        public UserRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<UserPermission> GetUserPermissions(string userId)
        {
            using (var connection = _context.CreateConnection())
            {
                var permissions = await connection.QuerySingleOrDefaultAsync<UserPermission>(
                    "sp_GetUserPermissions",
                    new { UserId = userId },
                    commandType: CommandType.StoredProcedure);

                return permissions;
            }
        }
    }
}