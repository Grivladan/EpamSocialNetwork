using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
            var currentUserId = User.Identity.GetUserId();
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            ViewBag.Text = "";
            return View(currentUser);
        }

        [HttpPost]
        public ActionResult GetUserById(FormCollection formCollection)
        {
            RedirectToAction("Log", "Account");
            Post post = new Post();
            post.Text = formCollection["Text"];
            context.Posts.Add(post);
            context.SaveChanges();
            ViewBag.Text = post.Text;
            return View("GetUserByID");
        }

        public ActionResult GetAllUsers()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var users = manager.Users.ToList();
            return View(users);
        }
    }
}