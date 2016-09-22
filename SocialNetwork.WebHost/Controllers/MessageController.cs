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
    public class MessageController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Message
        public ActionResult Index()
        {
            var messages = context.Messages.ToList();
            return View(messages);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Message message)
        {
            context.Messages.Add(message);
            context.SaveChanges();
            SendMessage("You get new message");
            return RedirectToAction("Index");
        }

        /*public ActionResult GetUserMessages(int id)
        {
            var messages = context.Messages.Where(x => x.ApplicationUserId == id || x.Sender.Id == id);
            return View(messages);
        }*/

        private void SendMessage(string message)
        {
            var context = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.All.displayMessage(message);
        }
    }
}