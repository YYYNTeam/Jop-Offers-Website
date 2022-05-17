using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using WebApplication2.Models;

[assembly: OwinStartupAttribute(typeof(WebApplication2.Startup))]
namespace WebApplication2
{
    public partial class Startup
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateDefaultRolesAndUsers();
        }

        public void CreateDefaultRolesAndUsers()
        {
            // to manage rules and store in the roleStore to manage groups
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            // the same to manage users
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole role = new IdentityRole();
            // if Admins role not in the db create new 
             if (!roleManager.RoleExists("Adminstrator"))
            {
                role.Name = "Adminstrator";
                roleManager.Create(role);
                ApplicationUser user = new ApplicationUser();
                user.UserName = "YYYN";
                user.Email = "softw7127@gmail.com";
                user.UserType = "Adminstrator";
                var Check = userManager.Create(user, "Softw@re404");
                if (Check.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Adminstrator");
                }
            }
             

        }
    }
}
