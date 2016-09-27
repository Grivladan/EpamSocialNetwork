using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject.Modules;
using SocialNetwork.DataAccess.Entities;

namespace SocialNetwork.Logic.Infrastructure
{
    public class LogicModule : NinjectModule
    {
        public override void Load()
        {
            //Kernel.Bind<IUserStore<ApplicationUser, int>>().To<UserStore<ApplicationUser>>();
           // Kernel.Bind<UserManager<ApplicationUser, int>>().ToSelf();
        }
    }
}
