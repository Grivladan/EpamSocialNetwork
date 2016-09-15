using SocialNetwork.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Logic.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAll();
        Task<Post> Get(int id);
        Task Create(Post post);
        Task<Post> Update(int id, Post post);
        Task Delete(int id);
        void Dispose();
    }
}
