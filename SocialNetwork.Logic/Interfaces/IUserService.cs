using SocialNetwork.DataAccess.Entities;
using System.Collections.Generic;

namespace SocialNetwork.Logic.Interfaces
{
    public interface IUserService
    {
        ApplicationUser GetById(int id);
        IEnumerable<ApplicationUser> GetAll();
        IEnumerable<ApplicationUser> Search(string searchString);
        IEnumerable<ApplicationUser> GetFriends(int id);
    }
}
