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

        public IEnumerable<Post> GetAll()
        {
            var posts = _unitOfWork.Posts.GetAll();
            return posts;
        }

        public Post GetById(int id)
        {
            var post = _unitOfWork.Posts.GetById(id);
            return post;           
        }


        public void Create(Post post)
        {
            _unitOfWork.Posts.Create(post);
            _unitOfWork.Save();
        }

        public Post Update(int id, Post post)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        /*public IEnumerable<Post> GetPostsByUser(ApplicationUser user)
        {
            return _unitOfWork.Posts.Query.Where(x => x.ApplicationUser == user);
        }*/

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
