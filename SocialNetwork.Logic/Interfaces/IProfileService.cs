using SocialNetwork.Logic.DTO;

namespace SocialNetwork.Logic.Interfaces
{
    public interface IProfileService
    {
        ProfileDTO GetById(int id);
        void Update(ProfileDTO profile);
    }
}
