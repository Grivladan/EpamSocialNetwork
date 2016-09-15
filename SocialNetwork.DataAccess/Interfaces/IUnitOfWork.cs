﻿using System;
using SocialNetwork.DataAccess.Entities;

namespace SocialNetwork.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Group> Groups { get;}
        IRepository<Message> Messages { get;}
        IRepository<Post> Posts { get;}
        IRepository<Profile> Profiles { get;}
       // IRepository<ApplicationUser> ApplicationUsers { get;}

        void Save();
    }
}
