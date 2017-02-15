using SocialNetwork.DataAccess.Entities;
using SocialNetwork.Logic.DTO;
using System.Collections.Generic;

namespace SocialNetwork.Logic.Interfaces
{
    public interface ICommentService
    {
        IEnumerable<CommentDTO> GetAll();
        CommentDTO Create(CommentDTO comment);
        IEnumerable<CommentDTO> GetCommentsToPost(int id);

        void Dispose();
    }
}
