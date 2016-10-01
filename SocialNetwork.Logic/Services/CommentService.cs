using Microsoft.AspNet.Identity;
using SocialNetwork.DataAccess.Entities;
using SocialNetwork.DataAccess.Interfaces;
using SocialNetwork.Logic.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.Logic.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser, int> _manager;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _manager = new UserManager<ApplicationUser, int>(new CustomUserStore(_unitOfWork.GetContext()));
        }

        public IEnumerable<Comment> GetAll()
        {
            var comments = _unitOfWork.Comments.Query;
            return comments;
        }

        public Comment Create(Comment comment)
        {
            comment.ApplicationUser = _manager.FindById(comment.ApplicationUserId);
            _unitOfWork.Comments.Create(comment);
            _unitOfWork.Save();

            return null;
        }

        public IEnumerable<Comment> GetCommentsToPost(int id)
        {
            var comments = _unitOfWork.Comments.Query.Where( x => x.PostId == id).ToList();
            return comments;
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
