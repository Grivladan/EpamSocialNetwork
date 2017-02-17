using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SocialNetwork.Logic.Interfaces;
using AutoMapper;
using SocialNetwork.WebHost.ViewModel;
using SocialNetwork.Logic.DTO;
using System.Collections.Generic;

namespace SocialNetwork.WebHost.Controllers
{
    public class FriendRequestController : Controller
    {
        private readonly IFriendRequestService _friendRequestService;

        public FriendRequestController(IFriendRequestService friendRequestService)
        {
           _friendRequestService = friendRequestService;
        }

        public ActionResult GetRequestsById(int id)
        {
            var requestsDto = _friendRequestService.GetRequestsById(id);
            Mapper.Initialize(cfg => cfg.CreateMap<FriendRequestDTO, FriendRequestViewModel>());
            var requestsViewModel = Mapper.Map<IEnumerable<FriendRequestDTO>, IEnumerable<FriendRequestViewModel>>(requestsDto);
            return View(requestsViewModel);
        }

        public ActionResult Create(int friendUserId)
        {
            var request = _friendRequestService.Create(User.Identity.GetUserId<int>(),friendUserId); 
            return RedirectToAction("GetUserFriends", "Home");
        }

        public ActionResult Reject(int id)
        {
            _friendRequestService.Reject(id);
            return RedirectToAction("GetUserFriends", "Home");
        }

        public ActionResult Accept(int id)
        {
            _friendRequestService.Accept(id);
            return RedirectToAction("GetUserFriends", "Home");
        }

        public ActionResult HasWaitingRequest(int id)
        {
            var count = _friendRequestService.HasWaitingRequest(id);

            return Json( new { countRequests = count },
                 JsonRequestBehavior.AllowGet);
        }
        
    }
}