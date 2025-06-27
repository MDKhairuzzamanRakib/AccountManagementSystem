using AccountManagementSystem.Models;
using Dapper;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagementSystem.Data
{
    public interface IAccountRepository
    {
        Task<int> CreateAccount(ChartOfAccount account, string userId);
        Task UpdateAccount(ChartOfAccount account);
        Task DeleteAccount(int accountId);
        Task<ChartOfAccount> GetAccount(int accountId);
        Task<IEnumerable<ChartOfAccount>> GetAllAccounts();
        Task<IEnumerable<AccountTreeItem>> GetAccountTree();
    }

    public class AccountRepository : IAccountRepository
    {
        private readonly DapperContext _context;

        public AccountRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAccount(ChartOfAccount account, string userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Action", "INSERT");
            parameters.Add("AccountCode", account.AccountCode);
            parameters.Add("AccountName", account.AccountName);
            parameters.Add("ParentAccountId", account.ParentAccountId);
            parameters.Add("AccountType", account.AccountType);
            parameters.Add("IsActive", account.IsActive);
            parameters.Add("UserId", userId);

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QuerySingleAsync<int>("sp_ManageChartOfAccounts", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task UpdateAccount(ChartOfAccount account)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Action", "UPDATE");
            parameters.Add("AccountId", account.AccountId);
            parameters.Add("AccountCode", account.AccountCode);
            parameters.Add("AccountName", account.AccountName);
            parameters.Add("ParentAccountId", account.ParentAccountId);
            parameters.Add("AccountType", account.AccountType);
            parameters.Add("IsActive", account.IsActive);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync("sp_ManageChartOfAccounts", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteAccount(int accountId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Action", "DELETE");
            parameters.Add("AccountId", accountId);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync("sp_ManageChartOfAccounts", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<ChartOfAccount> GetAccount(int accountId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Action", "GET");
            parameters.Add("AccountId", accountId);

            using (var connection = _context.CreateConnection())
            {
                var account = await connection.QuerySingleOrDefaultAsync<ChartOfAccount>("sp_ManageChartOfAccounts", parameters, commandType: CommandType.StoredProcedure);
                return account;
            }
        }

        public async Task<IEnumerable<ChartOfAccount>> GetAllAccounts()
        {
            var parameters = new DynamicParameters();
            parameters.Add("Action", "GETALL");

            using (var connection = _context.CreateConnection())
            {
                var accounts = await connection.QueryAsync<ChartOfAccount>("sp_ManageChartOfAccounts", parameters, commandType: CommandType.StoredProcedure);
                return accounts;
            }
        }

        public async Task<IEnumerable<AccountTreeItem>> GetAccountTree()
        {
            using (var connection = _context.CreateConnection())
            {
                var accounts = await connection.QueryAsync<AccountTreeItem>("sp_GetAccountTree", commandType: CommandType.StoredProcedure);
                return accounts;
            }
        }
    }
}