using SocialNetwork.DataAccess.Entities;
using SocialNetwork.DataAccess.Interfaces;
using SocialNetwork.Logic.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using SocialNetwork.Logic.DTO;
using AutoMapper;
using SocialNetwork.Logic.Infrastructure;
using System;

namespace SocialNetwork.Logic.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PostDTO> GetAll()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Post, PostDTO>());
            return Mapper.Map<IEnumerable<Post>, List<PostDTO>>(_unitOfWork.Posts.GetAll());
        }

        public PostDTO GetById(int id)
        {
            var post = _unitOfWork.Posts.GetById(id);
            if (post == null)
                throw new ValidationException("Post doesn't exist", "");
            Mapper.Initialize(cfg => cfg.CreateMap<Post, PostDTO>());
            return Mapper.Map<Post, PostDTO>(post);     
        }


        public void Create(PostDTO postDto)
        {
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<PostDTO, Post>());
                var post = Mapper.Map<PostDTO, Post>(postDto);
                post.ApplicationUser = _unitOfWork.UserManager.FindById(post.ApplicationUserId ?? 0);
                _unitOfWork.Posts.Create(post);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(int id, PostDTO newPostDto)
        {
            var post = _unitOfWork.Posts.GetById(id);
            if (post == null)
                throw new ValidationException("Post doesn't exist", "");

            Mapper.Initialize(cfg => cfg.CreateMap<PostDTO, Post>());
            var newPost = Mapper.Map<PostDTO, Post>(newPostDto);
            _unitOfWork.Posts.Update(newPost);
            _unitOfWork.Save();
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

        public IEnumerable<PostDTO> GetPostsByUser(int id)
        {
            var posts = _unitOfWork.Posts.Query.Where(x => x.ApplicationUserId == id).OrderByDescending(x => x.Date).ToList();
            Mapper.Initialize(cfg => cfg.CreateMap<Post, PostDTO>());
            return Mapper.Map<IEnumerable<Post>, List<PostDTO>>(posts);
        }

        public IEnumerable<PostDTO> GetFriendsPosts(int id)
        {
            var user = _unitOfWork.UserManager.FindById(id);
            if (user == null)
                throw new ValidationException("User doesn't exist", "");
            var friendIds = user.Friends.Select( p => p.Id);
            var posts = _unitOfWork.Posts.Query.Where( x => friendIds.Contains((int)x.ApplicationUserId)).ToList();
            Mapper.Initialize(cfg => cfg.CreateMap<Post, PostDTO>());
            return Mapper.Map<IEnumerable<Post>, List<PostDTO>>(posts);
        }

        public void LikePost(LikeDTO likeDto)
        {
            var like = _unitOfWork.Likes.Query.Where(x => x.PostId == likeDto.PostId && x.OwnerId == likeDto.OwnerId).FirstOrDefault();
            if (like == null)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<LikeDTO, Like>());
                like = Mapper.Map<LikeDTO, Like>(likeDto);
                like.Owner = _unitOfWork.UserManager.FindById(likeDto.OwnerId);
                like.Post = _unitOfWork.Posts.GetById(likeDto.PostId);
                _unitOfWork.Likes.Create(like);
            }
            else
            {
                _unitOfWork.Likes.Delete(like.Id);
            }
            _unitOfWork.Save();
        }
    }
}
