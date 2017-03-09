using AutoMapper;
using Microsoft.AspNet.Identity;
using SocialNetwork.DataAccess.Entities;
using SocialNetwork.DataAccess.Interfaces;
using SocialNetwork.Logic.DTO;
using SocialNetwork.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.Logic.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CommentDTO> GetAll()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Comment, CommentDTO>());
            return Mapper.Map<List<Comment>, List<CommentDTO>>(_unitOfWork.Comments.GetAll().ToList());
        }

        public CommentDTO Create(CommentDTO commentDto)
        {
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<CommentDTO, Comment>());
                var comment = Mapper.Map<CommentDTO, Comment>(commentDto);
                comment.ApplicationUser = _unitOfWork.UserManager.FindById(comment.ApplicationUserId);
                comment.Post = _unitOfWork.Posts.GetById(comment.PostId);
                _unitOfWork.Comments.Create(comment);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public IEnumerable<CommentDTO> GetCommentsToPost(int id)
        {
            var comments = _unitOfWork.Comments.Query.Where( x => x.PostId == id).OrderByDescending(x => x.CommentDate).ToList();
            Mapper.Initialize(cfg => cfg.CreateMap<Comment, CommentDTO>());
            return Mapper.Map<List<Comment>, List<CommentDTO>>(comments);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
