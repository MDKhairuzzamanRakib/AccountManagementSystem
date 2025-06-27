using AccountManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace AccountManagementSystem.Helpers
{
    public static class ViewHelper
    {
        public static List<AccountTreeItem> BuildAccountTree(IEnumerable<AccountTreeItem> items)
        {
            var lookup = items.ToLookup(x => x.ParentAccountId);

            foreach (var item in items)
            {
                item.Children = lookup[item.AccountId].OrderBy(x => x.AccountCode).ToList();
            }

            return lookup[null].OrderBy(x => x.AccountCode).ToList();
        }
    }
}