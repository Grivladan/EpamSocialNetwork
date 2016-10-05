using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SocialNetwork.DataAccess.Entities;
using System.Data.Entity;
using System.IO;

namespace SocialNetwork.DataAccess.EF
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser, int>(new CustomUserStore(context));
 
            var roleManager = new RoleManager<CustomRole, int>(new CustomRoleStore(context));
 
            var role1 = new CustomRole { Name = "admin" };
            var role2 = new CustomRole { Name = "user" };

            roleManager.Create(role1);
            roleManager.Create(role2);

            var admin = new ApplicationUser
            {
                Email = "grigorenkovlad1993@gmail.com",
                UserName = "grigorenkovlad1993@gmail.com", 
                Profile = new Profile { FirstName = "Vladyslav", LastName = "Hryhorenko"}
            };

            string password = "13101993Gv_";
            var result = userManager.Create(admin, password);
 
            if(result.Succeeded)
            {
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);
            }
 
            base.Seed(context);
        }
    }
}
