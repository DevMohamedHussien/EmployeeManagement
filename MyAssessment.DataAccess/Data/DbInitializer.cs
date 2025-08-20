using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MyAssessment.Core.Entities;
using MyAssessment.DataAccess.Data;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider service)
        {
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = service.GetRequiredService<UserManager<AppUser>>();

            string[] roles = { "User", "SuperAdmin", "Admin", "Manager", "Employee" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var superAdminEmail = "Omar@gmail.com";
            var user = await userManager.FindByEmailAsync(superAdminEmail);
            if (user == null)
            {
                var superAdmin = new AppUser
                {
                    UserName = superAdminEmail,
                    NormalizedUserName = superAdminEmail.ToUpper(),
                    Email = superAdminEmail,
                    NormalizedEmail = superAdminEmail.ToUpper(),
                    EmailConfirmed = false
                };

                var result = await userManager.CreateAsync(superAdmin, "P@$$w0rd");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
                }
            }

            // (Optional) Seed a Manager
            var managerEmail = "manager@test.com";
            var manager = await userManager.FindByEmailAsync(managerEmail);
            if (manager == null)
            {
                var newManager = new AppUser
                {
                    UserName = "manager",
                    Email = managerEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(newManager, "P@$$w0rd1");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newManager, "Manager");
                }
            }

            var employeeEmail = "employee@test.com";
            var employee = await userManager.FindByEmailAsync(employeeEmail);
            if (employee == null)
            {
                var newEmployee = new AppUser
                {
                    UserName = "employee",
                    Email = employeeEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(newEmployee, "P@$$w0rd2");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newEmployee, "Employee");
                }
            }
        }
    }
}
