using Microsoft.AspNet.Identity;
using SocialNetwork.DataAccess.Entities;
using SocialNetwork.DataAccess.Interfaces;
using SocialNetwork.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ApplicationUser GetById(int id)
        {
            var user = _unitOfWork.UserManager.FindById(id);
            return user;
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            var users = _unitOfWork.UserManager.Users.ToList();
            return users;
        }

        public IEnumerable<DataAccess.Entities.ApplicationUser> Search(string searchString, string country,
                        string city)
        {
            var users = _unitOfWork.UserManager.Users.ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(x => x.Profile.FirstName.Contains(searchString) 
                    || x.Profile.LastName.Contains(searchString)).ToList();
            }

            if (!String.IsNullOrEmpty(city))
            {
                users = users.Where(x => x.Profile.City != null && x.Profile.City.Contains(city)).ToList();
            }

            if (!String.IsNullOrEmpty(country))
            {
                users = users.Where( x => x.Profile.Country != null && x.Profile.Country.Contains(country)).ToList();
            }

            return users;
        }

        public IEnumerable<ApplicationUser> GetFriends(int id)
        {
            var currentUser = _unitOfWork.UserManager.FindById(id);
            return currentUser.Friends.ToList();
        }
    }
}
