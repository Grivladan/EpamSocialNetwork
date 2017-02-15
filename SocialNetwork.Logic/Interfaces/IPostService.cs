using SocialNetwork.DataAccess.Entities;
using SocialNetwork.Logic.DTO;
using System.Collections.Generic;

namespace SocialNetwork.Logic.Interfaces
{
    public interface IPostService
    {
        IEnumerable<PostDTO> GetAll();
        IEnumerable<PostDTO> GetFriendsPosts(int id);
        PostDTO GetById(int id);
        void Create(PostDTO post);
        PostDTO Update(int id, PostDTO post);
        void Delete(int id);
        IEnumerable<PostDTO> GetPostsByUser(int id);
        void LikePost(LikeDTO like);
        void Dispose();
    }
}
