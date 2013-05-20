using System.Configuration;
using System.Web.Mvc;
using TwitterFeeds.Models;
using TwitterFeeds.Service.Interface;

namespace TwitterFeeds.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApplicationService _service;

        private readonly string[] _accounts;

        public HomeController(IApplicationService service)
        {
            this._service = service;

            string firstAccount = ConfigurationManager.AppSettings["pay_by_phone"];
            string secondAccount = ConfigurationManager.AppSettings["PayByPhone"];
            string thirdAccount = ConfigurationManager.AppSettings["PayByPhone_UK"];

            string[] accounts = { firstAccount, secondAccount, thirdAccount };

            this._accounts = accounts;
        }

        // GET: /Home/
        public ActionResult Index()
        {
            TweetDataContract contract = this._service.GetAggregatedResults(this._accounts);

            return View(contract);
        }

        // GET: /Home/JsonView
        [HttpGet]
        public ActionResult RawData()
        {
            TweetDataContract contract = this._service.GetAggregatedResults(this._accounts);

            return Json(contract, JsonRequestBehavior.AllowGet);
        }

    }
}
