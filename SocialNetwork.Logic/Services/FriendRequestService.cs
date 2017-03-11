using SocialNetwork.DataAccess.Entities;
using SocialNetwork.DataAccess.Interfaces;
using SocialNetwork.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using SocialNetwork.Logic.DTO;
using AutoMapper;
using SocialNetwork.Logic.Infrastructure;

namespace SocialNetwork.Logic.Services
{
    public class FriendRequestService : IFriendRequestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FriendRequestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<FriendRequestDTO> GetRequestsById(int id)
        {
            var requests = _unitOfWork.Requests.Query.Where(x => x.RequestedTo == id && x.IsAccepted == false).ToList();
            Mapper.Initialize(cfg => cfg.CreateMap<FriendRequest, FriendRequestDTO>());
            return Mapper.Map<IEnumerable<FriendRequest>, List<FriendRequestDTO>>(requests);
        }

        public FriendRequest Create(int userId, int friendUserId)
        {
            try
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
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void Reject(int id)
        {
            try
            {
                _unitOfWork.Requests.Delete(id);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Accept(int id)
        {
            var request = _unitOfWork.Requests.Query.FirstOrDefault(x => x.Id == id);
            if (request == null)
                throw new ValidationException("request doesn't exist","");
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
