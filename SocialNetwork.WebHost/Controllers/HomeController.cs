using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using SocialNetwork.DataAccess.EF;
using SocialNetwork.DataAccess.Entities;
using SocialNetwork.WebHost.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.WebHost.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private ApplicationDbContext context;
        private UserManager<ApplicationUser, int> manager;

        public HomeController()
        {
            context = new ApplicationDbContext();
            manager = new UserManager<ApplicationUser, int>(new CustomUserStore(context));
        }

        public ActionResult GetUserById()
        {
            var currentUser = manager.FindById(User.Identity.GetUserId<int>());
            return View(currentUser);
        }

        [HttpPost]
        public ActionResult GetUserById(FormCollection formCollection)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId<int>());
            if (ModelState.IsValid)
            {
                Post post = new Post();
                post.Text = formCollection["Post"];
                post.ApplicationUser = currentUser;
                context.Posts.Add(post);
                context.SaveChanges();
            }
            return View("GetUserById", currentUser);
        }

        public ActionResult GetUserFriends(int page = 1, int pageSize = 1)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId<int>());

            PagedList<ApplicationUser> model = new PagedList<ApplicationUser>(currentUser.Friends, page, pageSize);

            return View("GetAllUsers", model );
        }

        public ActionResult GetAllUsers(int page = 1, int pageSize = 1)
        {
            var users = manager.Users.ToList();

            PagedList<ApplicationUser> model = new PagedList<ApplicationUser>(users, page, pageSize);

            return View(model);
        }

        public ActionResult Search(string searchString, int page = 1, int pageSize = 1)
        {
            var manager = new UserManager<ApplicationUser, int>(new CustomUserStore(context));
            var users = manager.Users.ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(x => x.Profile.FirstName.Contains(searchString)).ToList();
            }

            PagedList<ApplicationUser> model = new PagedList<ApplicationUser>(users, page, pageSize);

            return View("GetAllUsers", model);
        }

        public ActionResult AddFriend(int id)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId<int>());
            var requestedUser = manager.FindById(id);
            CreateFriendRequest(currentUser.Id, requestedUser.Id);
            currentUser.Friends.Add(requestedUser);
            /*requestedUser.Friends.Add(currentUser);
            manager.Update(currentUser);
            manager.Update(requestedUser);
            context.SaveChanges();*/
            return RedirectToAction("GetAllUsers");
        }

        public void CreateFriendRequest(int userId, int friendUserId)
        {
            var request = new FriendRequest()
            {
                RequestedFrom = userId,
                RequestedTo = friendUserId,
                Date = DateTime.Now
            };
            SendRequest();
            context.Requests.Add(request);
            context.SaveChanges();
        }

        private void SendRequest()
        {
            var context = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.All.displayMessage("You get friend request");
        }
    }
}