using SocialNetwork.DataAccess.Entities;
using SocialNetwork.DataAccess.Interfaces;
using SocialNetwork.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;

namespace SocialNetwork.Logic.Services
{
    public class FriendRequestService : IFriendRequestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FriendRequestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<FriendRequest> GetRequestsById(int id)
        {
            var requests = _unitOfWork.Requests.Query.Where(x => x.RequestedTo == id && x.IsAccepted == false).ToList();

            return requests;
        }

        public FriendRequest Create(int userId, int friendUserId)
        {
            var request = new FriendRequest()
            {
                ApplicationUser = _unitOfWork.UserManager.FindById(userId),
                RequestedTo = friendUserId,
                Date = DateTime.Now
            };
            _unitOfWork.Requests.Create(request);
            _unitOfWork.Save();
            return request;
        }

        public void Reject(int id)
        {
            var request = _unitOfWork.Requests.Query.FirstOrDefault(x => x.Id == id);
            _unitOfWork.Requests.Delete(id);
            _unitOfWork.Save();
        }

        public void Accept(int id)
        {
            var request = _unitOfWork.Requests.Query.FirstOrDefault(x => x.Id == id);
            var userFrom = _unitOfWork.UserManager.FindById(request.ApplicationUser.Id);
            var userTo = _unitOfWork.UserManager.FindById(request.RequestedTo);
            userFrom.Friends.Add(userTo);
            userTo.Friends.Add(userFrom);
            request.IsAccepted = true;
            _unitOfWork.Save();
        }

        public int HasWaitingRequest(int id)
        {
            var requests = _unitOfWork.Requests.Query.Where(x => x.RequestedTo == id && x.IsAccepted == false).ToList();

            return requests.Count;
        }
    }
}
