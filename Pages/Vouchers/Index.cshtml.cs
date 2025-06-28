using AccountManagementSystem.Data;
using AccountManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountManagementSystem.Services;

namespace AccountManagementSystem.Pages.Vouchers
{
    [Authorize(Roles = "Admin,Accountant,Viewer")]
    public class IndexModel : PageModel
    {
        private readonly IVoucherService _voucherService;

        public IndexModel(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        public IEnumerable<Voucher> Vouchers { get; set; }

        public async Task OnGetAsync()
        {
            Vouchers = await _voucherService.GetAllVouchers();
        }
    }
}