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
        public string CreatedBy { get; set; }
        public string ParentAccountName { get; set; }
    }

    public class AccountTreeItem : ChartOfAccount
    {
        public int Level { get; set; }
        public string DisplayName { get; set; }
        public List<AccountTreeItem> Children { get; set; } = new List<AccountTreeItem>();
    }


    public class UserPermission
    {
        public string Permissions { get; set; }
    }
}