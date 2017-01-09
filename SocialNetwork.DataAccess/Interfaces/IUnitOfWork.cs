using System;
using SocialNetwork.DataAccess.Entities;
using SocialNetwork.DataAccess.EF;
using Microsoft.AspNet.Identity;

namespace SocialNetwork.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Message> Messages { get;}
        IRepository<Post> Posts { get;}
        IRepository<Profile> Profiles { get;}
        IRepository<Comment> Comments { get; }
        IRepository<FriendRequest> Requests { get; }
        IRepository<Like> Likes { get; }
        UserManager<ApplicationUser, int> UserManager { get; }

        void Save();
    }
}
