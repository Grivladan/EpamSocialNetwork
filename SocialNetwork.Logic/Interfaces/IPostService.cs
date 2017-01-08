using SocialNetwork.DataAccess.Entities;
using System.Collections.Generic;

namespace SocialNetwork.Logic.Interfaces
{
    public interface IPostService
    {
        IEnumerable<Post> GetAll();
        Post GetById(int id);
        void Create(Post post);
        Post Update(int id, Post post);
        void Delete(int id);
        IEnumerable<Post> GetPostsByUser(int id);
        void LikePost(Like like);
        int LikeCount(int postId);
        void Dispose();
    }
}
