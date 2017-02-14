using SocialNetwork.DataAccess.Entities;
using SocialNetwork.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

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
        public ActionResult Create(FormCollection formCollection)
        {
            Comment comment = new Comment();
            comment.Text = formCollection["comment"];
            comment.PostId = Convert.ToInt32(formCollection["PostId"]);
            comment.ApplicationUserId = User.Identity.GetUserId<int>();
            _commentService.Create(comment);
            return RedirectToAction("GetCommentsToPost", new { id = comment.PostId});
        }

        public ActionResult GetCommentsToPost(int id)
        {
            var comments = _commentService.GetCommentsToPost(id);
            return View(comments);
        }
    }
}