using Microsoft.AspNet.Identity;
using SocialNetwork.DataAccess.EF;
using SocialNetwork.DataAccess.Entities;
using SocialNetwork.WebHost.Hubs;
using System.Linq;
using System.Web.Mvc;

namespace SocialNetwork.WebHost.Controllers
{
    public class MessageController : Controller
    {
        ApplicationDbContext context;
        UserManager<ApplicationUser, int> manager;

        public MessageController()
        {
            context =  new ApplicationDbContext();
            manager = new UserManager<ApplicationUser, int>(new CustomUserStore(context));
        }
        // GET: Message
        public ActionResult Index()
        {
            var messages = context.Messages.ToList();
            return View(messages);
        }

        public ActionResult Create(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Message message, int id)
        {
            message.ApplicationUser = manager.FindById(User.Identity.GetUserId<int>());
            var userTo = manager.FindById(id);
            message.Receiver = userTo;
            context.Messages.Add(message);
            context.SaveChanges();
            SendMessage("You get new message");
            return RedirectToAction("Index");
        }

        public ActionResult GetUserMessages(int id)
        {
            var messages = context.Messages.Where(x => x.ApplicationUserId == id || x.Receiver.Id == id);
            return View("Index", messages);
        }

        private void SendMessage(string message)
        {
            var context = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.All.displayMessage(message);
        }

        private void SendMessageById(string message, string connectionId)
        {
            var context = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.Client(connectionId).displayMessage(message);
        }
    }
}