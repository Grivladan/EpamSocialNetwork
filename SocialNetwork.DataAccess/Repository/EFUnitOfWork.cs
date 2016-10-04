﻿using System;
using SocialNetwork.DataAccess.EF;
using SocialNetwork.DataAccess.Entities;
using SocialNetwork.DataAccess.Interfaces;
using Microsoft.AspNet.Identity;

namespace SocialNetwork.DataAccess.Repository
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        private bool _isDisposed;
        private readonly IRepositoryFactory _repositoryFactory;

        public EFUnitOfWork(ApplicationDbContext context, IRepositoryFactory repositoryFactory)
        {
            _context = context;
            _repositoryFactory = repositoryFactory;
        }

        private UserManager<ApplicationUser, int> _manager;
        private IRepository<Message> _messagesRepository;
        private IRepository<Post> _postsRepository;
        private IRepository<Profile> _profilesRepository;
        private IRepository<Comment> _commentsRepository;
        private IRepository<FriendRequest> _requestsRepository;

        public new UserManager<ApplicationUser, int> UserManager
        {
            get
            {
                return _manager ?? (_manager = new UserManager<ApplicationUser, int>(new CustomUserStore(_context)));
            }
        }

        public IRepository<Message> Messages
        {
            get
            {
                return _messagesRepository ?? (_messagesRepository = _repositoryFactory.CreateRepository<Message>(_context));
            }
        }

        public IRepository<Post> Posts
        {
            get
            {
                return _postsRepository ?? (_postsRepository = _repositoryFactory.CreateRepository<Post>(_context));
            }
        }

        public IRepository<Profile> Profiles
        {
            get
            {
                return _profilesRepository ??
                       (_profilesRepository = _repositoryFactory.CreateRepository<Profile>(_context));
            }
        }


        public IRepository<Comment> Comments
        {
            get
            {
                return  _commentsRepository ??
                       (_commentsRepository = _repositoryFactory.CreateRepository<Comment>(_context));
            }
        }

        public IRepository<FriendRequest> Requests
        {
            get
            {
                return _requestsRepository ??
                       (_requestsRepository = _repositoryFactory.CreateRepository<FriendRequest>(_context));
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                    _manager.Dispose();
                }
            }

            _isDisposed = true;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
