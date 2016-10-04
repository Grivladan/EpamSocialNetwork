using Microsoft.AspNet.Identity;
using PagedList;
using SocialNetwork.DataAccess.Entities;
using SocialNetwork.Logic.Interfaces;
using System.Web.Mvc;

namespace SocialNetwork.WebHost.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPostService _postService;

        public HomeController(IUserService userService, IPostService postService)
        {
            _userService = userService;
            _postService = postService;
        }

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("GetUserById");
            return RedirectToAction("Login", "Account", null);
        }

        public ActionResult GetUserById()
        {
            var currentUser = _userService.GetById(User.Identity.GetUserId<int>());
            return View(currentUser);
        }

        [HttpPost]
        public ActionResult GetUserById(FormCollection formCollection)
        {
            var currentUser = _userService.GetById(User.Identity.GetUserId<int>());
            if (ModelState.IsValid)
            {
                Post post = new Post();
                post.Text = formCollection["Post"];
                post.ApplicationUser = currentUser;
                _postService.Create(post);
            }
            return View("GetUserById", currentUser);
        }

        public ActionResult GetUserFriends(int page = 1, int pageSize = 1)
        {
            var friends = _userService.GetFriends(User.Identity.GetUserId<int>());

            PagedList<ApplicationUser> model = new PagedList<ApplicationUser>(friends, page, pageSize);

            return View("GetAllUsers", model );
        }

        public ActionResult GetAllUsers(int page = 1, int pageSize = 1)
        {
            var users = _userService.GetAll();

            PagedList<ApplicationUser> model = new PagedList<ApplicationUser>(users, page, pageSize);

            return View(model);
        }

        public ActionResult Search(string searchString, int page = 1, int pageSize = 1)
        {
            var users = _userService.Search(searchString);

            PagedList<ApplicationUser> model = new PagedList<ApplicationUser>(users, page, pageSize);

            return View("GetAllUsers", model);
        }

        public ActionResult AutocompleteSearch(string term)
        {
            var users = _userService.Search(term);

            return Json(users, JsonRequestBehavior.AllowGet);
        }

       /* public ActionResult AddFriend(int id)
        {
            CreateFriendRequest(User.Identity.GetUserId<int>(), id);
            return RedirectToAction("GetAllUsers");
        }

        public void CreateFriendRequest(int userId, int friendUserId)
        {
            var request = new FriendRequest()
            {
                RequestedFrom = manager.FindById(userId),
                RequestedTo = manager.FindById(friendUserId),
                Date = DateTime.Now
            };

            context.Requests.Add(request);
            context.SaveChanges();
        }*/

    }
}