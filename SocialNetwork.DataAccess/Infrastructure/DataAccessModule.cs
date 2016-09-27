using SocialNetwork.DataAccess.Interfaces;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using SocialNetwork.DataAccess.EF;
using SocialNetwork.DataAccess.Repository;
using SocialNetwork.DataAccess.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject.Parameters;

namespace SocialNetwork.DataAccess.Infrastructure
{
    public class DataAccessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepositoryFactory>().ToFactory();
            Bind<ApplicationDbContext>().ToSelf();
            Bind(typeof(IRepository<>)).To(typeof(Repository<>));
            Bind<IUnitOfWork>().To<EFUnitOfWork>();
        }
    }
}
