using AccountManagementSystem.Data;
using AccountManagementSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagementSystem.Services
{
    public interface IAccountService
    {
        Task<int> CreateAccount(ChartOfAccount account);
        Task UpdateAccount(ChartOfAccount account);
        Task DeleteAccount(int accountId);
        Task<ChartOfAccount> GetAccount(int accountId);
        Task<IEnumerable<ChartOfAccount>> GetAllAccounts();
        Task<IEnumerable<AccountTreeItem>> GetAccountTree();
    }

    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Task<int> CreateAccount(ChartOfAccount account) => _accountRepository.CreateAccount(account);
        public Task UpdateAccount(ChartOfAccount account) => _accountRepository.UpdateAccount(account);
        public Task DeleteAccount(int accountId) => _accountRepository.DeleteAccount(accountId);
        public Task<ChartOfAccount> GetAccount(int accountId) => _accountRepository.GetAccount(accountId);
        public Task<IEnumerable<ChartOfAccount>> GetAllAccounts() => _accountRepository.GetAllAccounts();
        public Task<IEnumerable<AccountTreeItem>> GetAccountTree() => _accountRepository.GetAccountTree();
    }
}