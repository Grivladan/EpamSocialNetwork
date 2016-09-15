using SocialNetwork.DataAccess.Entities;
using SocialNetwork.DataAccess.Interfaces;
using SocialNetwork.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Logic.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            var posts = await _unitOfWork.Posts.GetAll();
            return posts;
        }

        public async Task<Post> Get(int id)
        {
            var post = await _unitOfWork.Posts.GetAsync(id);
            return post;           
        }


        public Task Create(Post post)
        {
            _unitOfWork.Posts.Create(post);
            _unitOfWork.Save();
            return null;
        }

        public Task<Post> Update(int id, Post post)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
