using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Areas.Identity.Data
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
                using (var context = new WebApplicationContext(
                    serviceProvider.GetRequiredService<DbContextOptions<WebApplicationContext>>()))
                {
                    // For sample purposes we are seeding 2 users both with the same password.
                    // The password is set with the following command:
                    // dotnet user-secrets set SeedUserPW <pw>
                    // The admin user can do anything

                    var adminID = await EnsureUser(serviceProvider, testUserPw, "admin@ingeniandonos.com");
                    await EnsureRole(serviceProvider, adminID, Constants.AdministratorsRole);

                    // allowed user can create and edit contacts that they create
                    var uid = await EnsureUser(serviceProvider, testUserPw, "manager@contoso.com");
                    await EnsureRole(serviceProvider, uid, Constants.ClientRole);

                    SeedDB(context, adminID);
                }
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<WebApplicationUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new WebApplicationUser { UserName = UserName };
                await userManager.CreateAsync(user, testUserPw);
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,string uid, string role)
        {
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<WebApplicationUser>>();

            var user = await userManager.FindByIdAsync(uid);

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
        public static void SeedDB(WebApplicationContext context, string adminID)
        {
            if (context.Contacts.Any())
            {
                return;   // DB has been seeded
            }

            context.Contacts.AddRange(
                new Contact
                {
                    Name = "Debra Garcia",
                    Address = "1234 Main St",
                    City = "Redmond",
                    State = "WA",
                    Zip = "10999",
                    Email = "debra@example.com",
                    Status = ContactStatus.Approved,
                    OwnerID = adminID
                });
        }
    }
}
