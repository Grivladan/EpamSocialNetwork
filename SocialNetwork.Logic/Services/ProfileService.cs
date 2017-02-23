using AutoMapper;
using SocialNetwork.DataAccess.Interfaces;
using SocialNetwork.Logic.DTO;
using SocialNetwork.Logic.Interfaces;

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
            Mapper.Initialize(cfg => cfg.CreateMap<DataAccess.Entities.Profile, ProfileDTO>());
            return Mapper.Map<DataAccess.Entities.Profile, ProfileDTO>(_unitOfWork.Profiles.GetById(id));
        }

        public void Update(ProfileDTO newProfileDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<ProfileDTO, DataAccess.Entities.Profile>());
            var newProfile = Mapper.Map<ProfileDTO, DataAccess.Entities.Profile>(newProfileDto);
            _unitOfWork.Profiles.Update(newProfile);
            _unitOfWork.Save();
        }
    }
}
