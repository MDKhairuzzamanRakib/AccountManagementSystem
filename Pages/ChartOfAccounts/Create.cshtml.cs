using AccountManagementSystem.Data;
using AccountManagementSystem.Models;
using AccountManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AccountManagementSystem.Pages.ChartOfAccounts
{
    [Authorize(Roles = "Admin,Accountant")]
    public class CreateModel : PageModel
    {
        private readonly IAccountService _accountService;

        public CreateModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public ChartOfAccount Account { get; set; }


        public SelectList ParentAccounts { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var accounts = await _accountService.GetAllAccounts();
            ParentAccounts = new SelectList(accounts, "AccountId", "AccountName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Account.CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!ModelState.IsValid)
            {
                var accounts = await _accountService.GetAllAccounts();
                ParentAccounts = new SelectList(accounts, "AccountId", "AccountName");
                return Page();
            }

            await _accountService.CreateAccount(Account);
            return RedirectToPage("./Index");
        }
    }
}