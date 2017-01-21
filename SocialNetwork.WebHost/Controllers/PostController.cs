﻿using Microsoft.AspNet.Identity;
using SocialNetwork.DataAccess.Entities;
using SocialNetwork.Logic.Interfaces;
using System.Web.Mvc;

namespace SocialNetwork.WebHost.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        public ActionResult GetPostsByUser(int id)
        {
            var posts = _postService.GetPostsByUser(id);
            return View(posts);
        }

        public ActionResult Create(FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                Post post = new Post();
                post.Text = formCollection["Post"];
                post.ApplicationUserId = User.Identity.GetUserId<int>();
                _postService.Create(post);
            }
            return RedirectToAction("GetUserById", "Home");
        }

        [HttpPost]
        public ActionResult LikePost(int postId)
        {
            var post = _postService.GetById(postId);
            Like like = new Like()
            {
                OwnerId = User.Identity.GetUserId<int>(),
                PostId = postId
            };
            _postService.LikePost(like);

            return PartialView("_LikeButton", post);
        }

        public ActionResult GetFriendsPosts(int id)
        {
            var posts = _postService.GetFriendsPosts(id);
            return View("GetPostsByUser", posts);
        }

    }
}