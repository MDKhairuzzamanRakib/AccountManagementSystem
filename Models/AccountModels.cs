using System;
using System.Collections.Generic;

namespace AccountManagementSystem.Models
{
    public class ChartOfAccount
    {
        public int AccountId { get; set; }
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public int? ParentAccountId { get; set; }
        public string AccountType { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? ParentAccountName { get; set; }
    }

    public class AccountTreeItem : ChartOfAccount
    {
        public int Level { get; set; }
        public string DisplayName { get; set; }
        public List<AccountTreeItem> Children { get; set; } = new List<AccountTreeItem>();
    }

    public class Voucher
    {
        public int VoucherId { get; set; }
        public string? VoucherType { get; set; }
        public string? VoucherNumber { get; set; }
        public DateTime VoucherDate { get; set; }
        public string Reference { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public List<VoucherDetail> Details { get; set; } = new List<VoucherDetail>();
    }

    public class VoucherDetail
    {
        public int VoucherDetailId { get; set; }
        public int? VoucherId { get; set; }
        public int? AccountId { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public string Description { get; set; }
        public string AccountName { get; set; }
        public string AccountCode { get; set; }
    }

    public class VoucherViewModel
    {
        public Voucher Voucher { get; set; } = new Voucher();
        public List<ChartOfAccount> Accounts { get; set; } = new List<ChartOfAccount>();
    }

    public class UserPermission
    {
        public string Permissions { get; set; }
    }
}