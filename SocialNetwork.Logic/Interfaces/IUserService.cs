using SocialNetwork.DataAccess.Entities;
using System.Collections.Generic;

namespace SocialNetwork.Logic.Interfaces
{
    public interface IUserService
    {
        ApplicationUser GetById(int id);
        IEnumerable<ApplicationUser> GetAll();
        IEnumerable<ApplicationUser> Search(string searchString, string city, string country);
        IEnumerable<ApplicationUser> GetFriends(int id);
    }
}
