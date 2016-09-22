using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using SocialNetwork.DataAccess.EF;
using SocialNetwork.DataAccess.Entities;
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
        ApplicationDbContext context = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetUserById()
        {
            var currentUserId = User.Identity.GetUserId<int>();
            var manager = new UserManager<ApplicationUser, int>(new CustomUserStore(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId<int>());
            return View(currentUser);
        }

        [HttpPost]
        public ActionResult GetUserById(FormCollection formCollection)
        {
            var currentUserId = User.Identity.GetUserId<int>();
            var manager = new UserManager<ApplicationUser, int>(new CustomUserStore(context));
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

        public ActionResult GetAllUsers(int page = 1, int pageSize = 1)
        {
            var manager = new UserManager<ApplicationUser, int>(new CustomUserStore(context));
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
    }
}