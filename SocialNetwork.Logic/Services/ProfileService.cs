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
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProfileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Profile GetById(int id)
        {
            var profile = _unitOfWork.Profiles.GetById(id);

            return profile;
        }

        public void Update(int id, Profile newProfile)
        {
            var profile = _unitOfWork.Profiles.GetById(id);

            _unitOfWork.Profiles.Update(newProfile);
        }
    }
}
