using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Jop_Offers_Website.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplication2.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string UserType { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Jop_Offers_Website.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<Jop_Offers_Website.Models.Job> Jobs { get; set; }

        public System.Data.Entity.DbSet<something.Models.ApplyForJob> ApplyForJobs { get; set; }

        public System.Data.Entity.DbSet<Jop_Offers_Website.Models.RoleViewModel> RoleViewModels { get; set; }
    }
}