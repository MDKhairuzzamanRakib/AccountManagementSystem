using AccountManagementSystem.Data;
using AccountManagementSystem.Models;
using AccountManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AccountManagementSystem.Pages.Vouchers
{
    [Authorize(Roles = "Admin,Accountant")]
    public class PaymentVoucherModel : PageModel
    {
        private readonly IVoucherService _voucherService;
        private readonly IAccountService _accountService;

        public PaymentVoucherModel(IVoucherService voucherService, IAccountService accountService)
        {
            _voucherService = voucherService;
            _accountService = accountService;
        }

        [BindProperty]
        public VoucherViewModel VoucherViewModel { get; set; } = new VoucherViewModel();

        public SelectList Accounts { get; set; }

        public async Task OnGetAsync()
        {
            var accounts = await _accountService.GetAllAccounts();
            Accounts = new SelectList(accounts, "AccountId", "AccountName");

            // Initialize with empty details
            VoucherViewModel.Voucher.VoucherType = "PAYMENT";
            VoucherViewModel.Voucher.VoucherDate = DateTime.Today;
            VoucherViewModel.Voucher.Details.Add(new VoucherDetail());
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var accounts = await _accountService.GetAllAccounts();
                Accounts = new SelectList(accounts, "AccountId", "AccountName");
                return Page();
            }

            VoucherViewModel.Voucher.CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // Generate voucher number
            VoucherViewModel.Voucher.VoucherNumber = $"PV-{DateTime.Today:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 4).ToUpper()}";

            await _voucherService.SaveVoucher(VoucherViewModel.Voucher);
            return RedirectToPage("./Index");
        }

        public IActionResult OnPostAddDetail()
        {
            VoucherViewModel.Voucher.Details.Add(new VoucherDetail());
            return Partial("_VoucherDetailRows", VoucherViewModel);
        }

        public IActionResult OnPostRemoveDetail(int index)
        {
            VoucherViewModel.Voucher.Details.RemoveAt(index);
            return Partial("_VoucherDetailRows", VoucherViewModel);
        }
    }
}