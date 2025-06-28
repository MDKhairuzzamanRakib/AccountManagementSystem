using AccountManagementSystem.Data;
using AccountManagementSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagementSystem.Services
{
    public interface IVoucherService
    {
        Task<int> SaveVoucher(Voucher voucher);
        Task<Voucher> GetVoucher(int voucherId);
        Task<IEnumerable<Voucher>> GetAllVouchers();
    }

    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepository _voucherRepository;
        private readonly IAccountRepository _accountRepository;

        public VoucherService(IVoucherRepository voucherRepository, IAccountRepository accountRepository)
        {
            _voucherRepository = voucherRepository;
            _accountRepository = accountRepository;
        }

        public Task<int> SaveVoucher(Voucher voucher) => _voucherRepository.SaveVoucher(voucher);
        public Task<Voucher> GetVoucher(int voucherId) => _voucherRepository.GetVoucher(voucherId);
        public Task<IEnumerable<Voucher>> GetAllVouchers() => _voucherRepository.GetAllVouchers();
    }
}