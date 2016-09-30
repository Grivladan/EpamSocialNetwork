using SocialNetwork.DataAccess.Entities;
using SocialNetwork.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.WebHost.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public ActionResult Create(Comment comment)
        {
            _commentService.Create(comment);
            return RedirectToAction("GetUserById", "Home");
        }

        public ActionResult GetCommentsToPost(int id)
        {
            var comments = _commentService.GetCommentsToPost(id);
            return View(comments);
        }
    }
}