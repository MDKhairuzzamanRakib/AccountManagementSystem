using AccountManagementSystem.Data;
using AccountManagementSystem.Helpers;
using AccountManagementSystem.Models;
using AccountManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagementSystem.Pages.ChartOfAccounts
{
    [Authorize(Roles = "Admin,Accountant,Viewer")]
    public class TreeViewModel : PageModel
    {
        private readonly IAccountService _accountService;

        public TreeViewModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public List<AccountTreeItem> AccountTree { get; set; }

        public async Task OnGetAsync()
        {
            var accounts = await _accountService.GetAccountTree();
            AccountTree = ViewHelper.BuildAccountTree(accounts);
        }
    }
}