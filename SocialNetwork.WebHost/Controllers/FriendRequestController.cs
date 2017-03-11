using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SocialNetwork.Logic.Interfaces;
using AutoMapper;
using SocialNetwork.WebHost.ViewModel;
using SocialNetwork.Logic.DTO;
using System.Collections.Generic;
using System;
using SocialNetwork.Logic.Infrastructure;

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
            try
            {
                var request = _friendRequestService.Create(User.Identity.GetUserId<int>(), friendUserId);
                return RedirectToAction("GetUserFriends", "Home");
            }
            catch(Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult Reject(int id)
        {
            try
            {
                _friendRequestService.Reject(id);
                return RedirectToAction("GetUserFriends", "Home");
            }
            catch(ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult Accept(int id)
        {
            try
            {
                _friendRequestService.Accept(id);
                return RedirectToAction("GetUserFriends", "Home");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult HasWaitingRequest(int id)
        {
            var count = _friendRequestService.HasWaitingRequest(id);

            return Json( new { countRequests = count },
                 JsonRequestBehavior.AllowGet);
        }
        
    }
}