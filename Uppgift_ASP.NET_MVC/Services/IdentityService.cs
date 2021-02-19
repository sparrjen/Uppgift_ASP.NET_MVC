using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uppgift_ASP.NET_MVC.Data;

namespace Uppgift_ASP.NET_MVC.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public IdentityService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task AddUserToRole(AppUser user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task CreateNewRole(string roleName)
        {
            await _roleManager.CreateAsync(new IdentityRole(roleName));
        }

        public async Task<IdentityResult> CreateNewUserAsync(AppUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task CreateRootAccountAsync()
        {
            if (!_userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    UserName = "admin@domain.com",
                    Email = "admin@domain.com",
                    FirstName = "Admin",
                    LastName = "Account"
                };
                var result = await _userManager.CreateAsync(user, "BytMig123!");

                if (result.Succeeded)
                {
                    if (!_roleManager.Roles.Any())
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Admin"));
                        await _roleManager.CreateAsync(new IdentityRole("Teacher"));
                        await _roleManager.CreateAsync(new IdentityRole("Student"));
                    }

                    await _userManager.AddToRoleAsync(user, "Admin");
                    await _signInManager.SignInAsync(user, isPersistent: false);

                }
            }
        }


        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return _roleManager.Roles;
        }

        public IEnumerable<AppUser> GetAllUsers()
        {
            return _userManager.Users;
        }
    }
}
