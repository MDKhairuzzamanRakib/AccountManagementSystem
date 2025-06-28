using AccountManagementSystem.Models;
using Dapper;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagementSystem.Data
{
    public interface IVoucherRepository
    {
        Task<int> SaveVoucher(Voucher voucher);
        Task<Voucher> GetVoucher(int voucherId);
        Task<IEnumerable<Voucher>> GetAllVouchers();
    }

    public class VoucherRepository : IVoucherRepository
    {
        private readonly DapperContext _context;

        public VoucherRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> SaveVoucher(Voucher voucher)
        {
            var parameters = new DynamicParameters();
            parameters.Add("VoucherType", voucher.VoucherType);
            parameters.Add("VoucherNumber", voucher.VoucherNumber);
            parameters.Add("VoucherDate", voucher.VoucherDate);
            parameters.Add("Reference", voucher.Reference);
            parameters.Add("Notes", voucher.Notes);
            parameters.Add("UserId", voucher.CreatedBy);

            // Create a DataTable for the details
            var detailsTable = new DataTable();
            detailsTable.Columns.Add("AccountId", typeof(int));
            detailsTable.Columns.Add("DebitAmount", typeof(decimal));
            detailsTable.Columns.Add("CreditAmount", typeof(decimal));
            detailsTable.Columns.Add("Description", typeof(string));

            foreach (var detail in voucher.Details)
            {
                detailsTable.Rows.Add(detail.AccountId, detail.DebitAmount, detail.CreditAmount, detail.Description);
            }

            parameters.Add("Details", detailsTable.AsTableValuedParameter("dbo.VoucherDetailType"));

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QuerySingleAsync<int>("sp_SaveVoucher", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<Voucher> GetVoucher(int voucherId)
        {
            using (var connection = _context.CreateConnection())
            {
                // Get voucher header
                var voucher = await connection.QuerySingleOrDefaultAsync<Voucher>(
                    "SELECT * FROM Vouchers WHERE VoucherId = @VoucherId",
                    new { VoucherId = voucherId });

                if (voucher != null)
                {
                    // Get voucher details with account names
                    var details = await connection.QueryAsync<VoucherDetail>(
                        @"SELECT vd.*, coa.AccountName, coa.AccountCode 
                          FROM VoucherDetails vd
                          INNER JOIN ChartOfAccounts coa ON vd.AccountId = coa.AccountId
                          WHERE vd.VoucherId = @VoucherId",
                        new { VoucherId = voucherId });

                    voucher.Details = details.AsList();
                }

                return voucher;
            }
        }

        public async Task<IEnumerable<Voucher>> GetAllVouchers()
        {
            using (var connection = _context.CreateConnection())
            {
                var vouchers = await connection.QueryAsync<Voucher>("SELECT * FROM Vouchers ORDER BY VoucherDate DESC");

                foreach (var voucher in vouchers)
                {
                    var details = await connection.QueryAsync<VoucherDetail>(
                        @"SELECT vd.*, coa.AccountName, coa.AccountCode 
                          FROM VoucherDetails vd
                          INNER JOIN ChartOfAccounts coa ON vd.AccountId = coa.AccountId
                          WHERE vd.VoucherId = @VoucherId",
                        new { VoucherId = voucher.VoucherId });

                    voucher.Details = details.AsList();
                }

                return vouchers;
            }
        }
    }
}