using System;
using SocialNetwork.DataAccess.Entities;
using SocialNetwork.DataAccess.EF;

namespace SocialNetwork.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Message> Messages { get;}
        IRepository<Post> Posts { get;}
        IRepository<Profile> Profiles { get;}

        ApplicationDbContext GetContext();
        void Save();
    }
}
