using SocialNetwork.DataAccess.Entities;
using SocialNetwork.DataAccess.Interfaces;
using SocialNetwork.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNet.Identity;
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
             post.ApplicationUser = _unitOfWork.UserManager.FindById(post.ApplicationUserId??0);
            _unitOfWork.Posts.Create(post);
            _unitOfWork.Save();
        }

        public Post Update(int id, Post newPost)
        {
            var post = _unitOfWork.Posts.GetById(id);
            if (post == null)
                return null;

            post.Text = newPost.Text;
            _unitOfWork.Save();
            return post;
        }

        public void Delete(int id)
        {
            _unitOfWork.Posts.Delete(id);
            _unitOfWork.Save();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public IEnumerable<Post> GetPostsByUser(int id)
        {
            var posts = _unitOfWork.Posts.Query.Where(x => x.ApplicationUserId == id).OrderByDescending(x => x.Date).ToList();
            return posts;
        }

        public void LikePost(Like like)
        {
            like.Owner = _unitOfWork.UserManager.FindById(like.OwnerId);
            like.Post = _unitOfWork.Posts.GetById(like.PostId);
            like.Post.Likes.Add(like);
            like.Owner.Likes.Add(like);
            _unitOfWork.Save();
        }
    }
}
