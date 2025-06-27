using AccountManagementSystem.Data;
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
    public class IndexModel : PageModel
    {
        private readonly IAccountService _accountService;

        public IndexModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IEnumerable<ChartOfAccount> Accounts { get; set; }

        public async Task OnGetAsync()
        {
            Accounts = await _accountService.GetAllAccounts();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            if (!User.IsInRole("Admin") && !User.IsInRole("Accountant"))
            {
                return Forbid();
            }

            await _accountService.DeleteAccount(id);
            return RedirectToPage();
        }
    }
}