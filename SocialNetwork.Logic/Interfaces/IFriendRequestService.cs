using SocialNetwork.DataAccess.Entities;
using SocialNetwork.Logic.DTO;
using System.Collections.Generic;

namespace SocialNetwork.Logic.Interfaces
{
    public interface IFriendRequestService
    {
        IEnumerable<FriendRequestDTO> GetRequestsById(int id);
        FriendRequest Create(int userId, int friendUserId);
        void Reject(int id);
        void Accept(int id);
        int HasWaitingRequest(int id);
    }
}
