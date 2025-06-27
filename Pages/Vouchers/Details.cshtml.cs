using AccountManagementSystem.Data;
using AccountManagementSystem.Models;
using AccountManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace AccountManagementSystem.Pages.Vouchers
{
    [Authorize(Roles = "Admin,Accountant,Viewer")]
    public class DetailsModel : PageModel
    {
        private readonly IVoucherService _voucherService;

        public DetailsModel(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        public Voucher Voucher { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Voucher = await _voucherService.GetVoucher(id.Value);
            if (Voucher == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}