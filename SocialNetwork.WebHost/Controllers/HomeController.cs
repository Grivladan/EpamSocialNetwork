﻿using Microsoft.AspNet.Identity;
using PagedList;
using SocialNetwork.DataAccess.Entities;
using SocialNetwork.Logic.Interfaces;
using System.Web.Mvc;
using System.Linq;
using SocialNetwork.Logic.DTO;

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

        public ActionResult GetUserById(int? id)
        {
            var userId = id ?? User.Identity.GetUserId<int>();
            var currentUser = _userService.GetById(userId);
            return View(currentUser);
        }

        [HttpPost]
        public ActionResult GetUserById(FormCollection formCollection)
        {
            var currentUser = _userService.GetById(User.Identity.GetUserId<int>());
            if (ModelState.IsValid)
            {
                PostDTO postDto = new PostDTO
                {
                    Text = formCollection["Post"],
                    ApplicationUser = currentUser
                };
                _postService.Create(postDto);
            }
            return View("GetUserById", currentUser);
        }

        public ActionResult GetUserFriends(int page = 1, int pageSize = 3)
        {
            var friends = _userService.GetFriends(User.Identity.GetUserId<int>());

            PagedList<ApplicationUser> model = new PagedList<ApplicationUser>(friends, page, pageSize);

            return View(model);
        }

        public ActionResult GetAllUsers(int page = 1, int pageSize = 3)
        {
            var users = _userService.GetAll();

            PagedList<ApplicationUser> model = new PagedList<ApplicationUser>(users, page, pageSize);

            return View(model);
        }

        public ActionResult Search(string searchString, string country, string city,
            int page = 1, int pageSize = 1)
        {
            var users = _userService.Search(searchString, country, city);

            PagedList<ApplicationUser> model = new PagedList<ApplicationUser>(users, page, pageSize);

            return View("GetAllUsers", model);
        }

        public ActionResult AutocompleteSearch(string term, string country, string city)
        {
            var users = _userService.Search(term, country, city);
            var viewModel = users.Select( x => new {
                user = x.Profile.FirstName,
                country = x.Profile.Country,
                city = x.Profile.City
            });
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

    }
}