﻿using SocialNetwork.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SocialNetwork.Logic.DTO;
using AutoMapper;
using SocialNetwork.WebHost.ViewModel;

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
            try
            {
                CommentDTO commentDto = new CommentDTO()
                {
                    Text = formCollection["comment"],
                    PostId = Convert.ToInt32(formCollection["PostId"]),
                    ApplicationUserId = User.Identity.GetUserId<int>()
                };
                if (string.IsNullOrEmpty(commentDto.Text))
                {
                    ModelState.AddModelError("Text", "Comment text can't be empty");
                }
                _commentService.Create(commentDto);
            }
            catch (Exception)
            {
                ModelState.AddModelError("Text", "Can't create comment");
            }
            return RedirectToAction("GetCommentsToPost", new { id = Convert.ToInt32(formCollection["PostId"])});
        }

        public ActionResult GetCommentsToPost(int id)
        {
            var comments = _commentService.GetCommentsToPost(id);
            Mapper.Initialize(cfg => cfg.CreateMap<CommentDTO, CommentViewModel>());
            return View(Mapper.Map<IEnumerable<CommentDTO>, IEnumerable<CommentViewModel>>(comments));
        }
    }
}