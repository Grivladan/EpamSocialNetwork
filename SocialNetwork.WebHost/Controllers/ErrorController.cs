using System.Web.Mvc;

namespace SocialNetwork.WebHost.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }

        public ActionResult Forbidden()
        {
            Response.StatusCode = 403;
            return View();
        }

        public ActionResult ServerError()
        {
            Response.StatusCode = 500;
            return View();
        }
    }
}