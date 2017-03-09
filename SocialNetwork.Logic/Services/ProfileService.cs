using AutoMapper;
using SocialNetwork.DataAccess.Interfaces;
using SocialNetwork.Logic.DTO;
using SocialNetwork.Logic.Infrastructure;
using SocialNetwork.Logic.Interfaces;
using System;

namespace SocialNetwork.Logic.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProfileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ProfileDTO GetById(int id)
        {
            var profile = _unitOfWork.Profiles.GetById(id);
            if (profile == null)
                throw new ValidationException("Profile doesn't exist", "");
            Mapper.Initialize(cfg => cfg.CreateMap<DataAccess.Entities.Profile, ProfileDTO>());
            return Mapper.Map<DataAccess.Entities.Profile, ProfileDTO>(profile);
        }

        public void Update(ProfileDTO newProfileDto)
        {
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<ProfileDTO, DataAccess.Entities.Profile>());
                var newProfile = Mapper.Map<ProfileDTO, DataAccess.Entities.Profile>(newProfileDto);
                _unitOfWork.Profiles.Update(newProfile);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
