using AccountManagementSystem.Data;
using AccountManagementSystem.Models;
using AccountManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace AccountManagementSystem.Pages.ChartOfAccounts
{
    [Authorize(Roles = "Admin,Accountant")]
    public class EditModel : PageModel
    {
        private readonly IAccountService _accountService;

        public EditModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public ChartOfAccount Account { get; set; }

        public SelectList ParentAccounts { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Account = await _accountService.GetAccount(id.Value);
            if (Account == null)
            {
                return NotFound();
            }

            var accounts = await _accountService.GetAllAccounts();
            ParentAccounts = new SelectList(accounts, "AccountId", "AccountName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var accounts = await _accountService.GetAllAccounts();
                ParentAccounts = new SelectList(accounts, "AccountId", "AccountName");
                return Page();
            }

            await _accountService.UpdateAccount(Account);
            return RedirectToPage("./Index");
        }
    }
}