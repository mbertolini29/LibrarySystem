using LibrarySystem.Models;
using LibrarySystem.Repositories.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Utilities.Seeding
{
    public class DbInitializer : IDbInitializer
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _context;

        public DbInitializer(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public void Initialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception)
            {

                throw;
            }
            //administrador de funciones
            if(!_roleManager.RoleExistsAsync(WebSiteRole.WebSite_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(WebSiteRole.WebSite_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebSiteRole.WebSite_User)).GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email = "adimin@gmail.com"
                }, "Admin@123").GetAwaiter().GetResult();
                var appuser = _userManager.Users.Where(x => x.Email == "admin@gmail.com").FirstOrDefault();
                if (appuser != null)
                {
                    _userManager.AddToRoleAsync(appuser, WebSiteRole.WebSite_Admin).GetAwaiter().GetResult();
                }

            }
        }
    }
}