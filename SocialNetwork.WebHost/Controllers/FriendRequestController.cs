using SocialNetwork.DataAccess.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SocialNetwork.DataAccess.Entities;

namespace SocialNetwork.WebHost.Controllers
{
    public class FriendRequestController : Controller
    {
        // GET: Friend
        ApplicationDbContext context;
        UserManager<ApplicationUser, int> _manager;

        public FriendRequestController()
        {
            context = new ApplicationDbContext();
            _manager = new UserManager<ApplicationUser, int>(new CustomUserStore(context));
        }

        public ActionResult GetRequestsById(int id)
        {
            var requests = context.Requests.Where( x => x.RequestedTo == id && x.IsAccepted == false).ToList();
            return View(requests);
        }

        public ActionResult Create(int friendUserId)
        {
            var request = new FriendRequest()
            {
                ApplicationUser = _manager.FindById(User.Identity.GetUserId<int>()),
                RequestedTo = friendUserId,
                Date = DateTime.Now
            };
            context.Requests.Add(request);
            context.SaveChanges();
            return RedirectToAction("GetUserFriends", "Home");
        }

        public ActionResult Reject(int id)
        {
            var request = context.Requests.FirstOrDefault( x => x.Id == id);
            context.Requests.Remove(request);
            context.SaveChanges();
            return RedirectToAction("GetUserFriends", "Home");
        }

        public ActionResult Accept(int id)
        {
            var request = context.Requests.FirstOrDefault( x => x.Id == id);
            var userFrom = _manager.FindById(request.ApplicationUser.Id);
            var userTo = _manager.FindById(request.RequestedTo);
            userFrom.Friends.Add(userTo);
            userTo.Friends.Add(userFrom);
            request.IsAccepted = true;
            context.SaveChanges();
            return RedirectToAction("GetUserFriends", "Home");
        }

        public ActionResult HasWaitingRequest(int id)
        {
            var requests = context.Requests.Where(x => x.RequestedTo == id && x.IsAccepted == false).ToList();

            return Json( new { countRequests = requests.Count },
                 JsonRequestBehavior.AllowGet);
        }
        
    }
}