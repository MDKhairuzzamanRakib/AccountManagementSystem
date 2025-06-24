using AccountManagementSystem.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AccountManagementSystem.Data
{
    public class DbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Initialize()
        {
            // Apply any pending migrations
            await _context.Database.MigrateAsync();

            // Seed roles
            await SeedRoles();

            // Seed admin user
            await SeedAdminUser();

            // Seed sample accounts if none exist
            await SeedSampleAccounts();
        }

        private async Task SeedRoles()
        {
            string[] roleNames = { "Admin", "Accountant", "Viewer" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        private async Task SeedAdminUser()
        {
            var adminUser = new IdentityUser
            {
                UserName = "admin@example.com",
                Email = "admin@example.com",
                EmailConfirmed = true
            };

            var user = await _userManager.FindByEmailAsync(adminUser.Email);
            if (user == null)
            {
                var createPowerUser = await _userManager.CreateAsync(adminUser, "Admin@123");
                if (createPowerUser.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }

        private async Task SeedSampleAccounts()
        {
            // Check if any accounts exist
            var accountsExist = await DatabaseHelper.ExecuteScalarAsync<int>(
                _context.Database.GetDbConnection(),
                "SELECT COUNT(*) FROM ChartOfAccounts") > 0;

            if (!accountsExist)
            {
                // Insert sample chart of accounts
                var userId = (await _userManager.FindByEmailAsync("admin@example.com"))?.Id;

                if (userId != null)
                {
                    await DatabaseHelper.ExecuteNonQueryAsync(
                        _context.Database.GetDbConnection(),
                        "EXEC sp_ManageChartOfAccounts @Action, @AccountCode, @AccountName, @AccountType, @ParentId, @IsActive, @UserId",
                        new Dictionary<string, object>
                        {
                            { "@Action", "INSERT" },
                            { "@AccountCode", "1000" },
                            { "@AccountName", "Assets" },
                            { "@AccountType", "Header" },
                            { "@ParentId", null },
                            { "@IsActive", true },
                            { "@UserId", userId }
                        });

                    // Get the ID of the Assets account
                    var assetsId = await DatabaseHelper.ExecuteScalarAsync<int>(
                        _context.Database.GetDbConnection(),
                        "SELECT AccountId FROM ChartOfAccounts WHERE AccountCode = '1000'");

                    // Insert some sample accounts under Assets
                    await DatabaseHelper.ExecuteNonQueryAsync(
                        _context.Database.GetDbConnection(),
                        "EXEC sp_ManageChartOfAccounts @Action, @AccountCode, @AccountName, @AccountType, @ParentId, @IsActive, @UserId",
                        new Dictionary<string, object>
                        {
                            { "@Action", "INSERT" },
                            { "@AccountCode", "1100" },
                            { "@AccountName", "Current Assets" },
                            { "@AccountType", "Header" },
                            { "@ParentId", assetsId },
                            { "@IsActive", true },
                            { "@UserId", userId }
                        });

                    var currentAssetsId = await DatabaseHelper.ExecuteScalarAsync<int>(
                        _context.Database.GetDbConnection(),
                        "SELECT AccountId FROM ChartOfAccounts WHERE AccountCode = '1100'");

                    await DatabaseHelper.ExecuteNonQueryAsync(
                        _context.Database.GetDbConnection(),
                        "EXEC sp_ManageChartOfAccounts @Action, @AccountCode, @AccountName, @AccountType, @ParentId, @IsActive, @UserId",
                        new Dictionary<string, object>
                        {
                            { "@Action", "INSERT" },
                            { "@AccountCode", "1110" },
                            { "@AccountName", "Cash" },
                            { "@AccountType", "Asset" },
                            { "@ParentId", currentAssetsId },
                            { "@IsActive", true },
                            { "@UserId", userId }
                        });

                    await DatabaseHelper.ExecuteNonQueryAsync(
                        _context.Database.GetDbConnection(),
                        "EXEC sp_ManageChartOfAccounts @Action, @AccountCode, @AccountName, @AccountType, @ParentId, @IsActive, @UserId",
                        new Dictionary<string, object>
                        {
                            { "@Action", "INSERT" },
                            { "@AccountCode", "1120" },
                            { "@AccountName", "Bank" },
                            { "@AccountType", "Asset" },
                            { "@ParentId", currentAssetsId },
                            { "@IsActive", true },
                            { "@UserId", userId }
                        });

                    // Add more sample accounts as needed...
                }
            }
        }
    }
}