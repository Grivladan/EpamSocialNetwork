using SocialNetwork.DataAccess.Entities;
using System.Collections.Generic;

namespace SocialNetwork.Logic.Interfaces
{
    public interface ICommentService
    {
        IEnumerable<Comment> GetAll();
        Comment Create(Comment comment);
        IEnumerable<Comment> GetCommentsToPost(int id);

        void Dispose();
    }
}
