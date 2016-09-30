using SocialNetwork.DataAccess.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.WebHost.Controllers
{
    public class PostController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public ActionResult GetPostsByUser(int id)
        {
            var posts = context.Posts.Where(x => x.ApplicationUserId == id).ToList();
            return View(posts);
        }
    }
}