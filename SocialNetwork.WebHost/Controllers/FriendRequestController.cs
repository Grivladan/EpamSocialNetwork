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
        ApplicationDbContext context = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(int friendUserId)
        {
            var request = new FriendRequest()
            {
                RequestedFrom = User.Identity.GetUserId<int>(),
                RequestedTo = friendUserId,
                Date = DateTime.Now
            };
            context.Requests.Add(request);
            context.SaveChanges();

            return null;
        }

        public ActionResult HasWaitingRequest(int id)
        {
            var requests = context.Requests.Where(x => x.RequestedTo == id).ToList();
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