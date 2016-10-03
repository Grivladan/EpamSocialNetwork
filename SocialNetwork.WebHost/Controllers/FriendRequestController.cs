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

        public ActionResult GetRequests()
        {
            var requests = context.Requests.Where(x => x.IsAccepted == false).ToList();
            return View(requests);
        }

        [HttpPost]
        public ActionResult Create(int friendUserId)
        {
            var request = new FriendRequest()
            {
                RequestedFrom = _manager.FindById(User.Identity.GetUserId<int>()),
                RequestedTo = _manager.FindById(friendUserId),
                Date = DateTime.Now
            };
            context.Requests.Add(request);
            context.SaveChanges();

            return null;
        }

        public ActionResult HasWaitingRequest(int id)
        {
            var requests = context.Requests.Where(x => x.RequestedTo.Id == id && x.IsAccepted == false).ToList();
            if (requests.Count !=0)
            {
                return Json( new { HasRequest = true },
                    JsonRequestBehavior.AllowGet);
            }

            return Json( new { HasRequest = false },
                 JsonRequestBehavior.AllowGet);
        }
        
    }
}