using SocialNetwork.DataAccess.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SocialNetwork.DataAccess.Entities;
using SocialNetwork.Logic.Interfaces;

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
            var requests = _friendRequestService.GetRequestsById(id);

            return View(requests);
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