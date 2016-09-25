﻿using System;
using SocialNetwork.DataAccess.Entities;

namespace SocialNetwork.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Message> Messages { get;}
        IRepository<Post> Posts { get;}
        IRepository<Profile> Profiles { get;}
        void Save();
    }
}
