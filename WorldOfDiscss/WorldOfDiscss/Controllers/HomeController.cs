using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorldOfDiscs.Models;
using PagedList;
using PagedList.Mvc;
using log4net;

namespace WorldOfDiscs.Controllers
{
    public class HomeController : Controller
    {
        //cấu hình ghi log 
        private static log4net.ILog Log { get; set; }
        ILog log = log4net.LogManager.GetLogger(typeof(HomeController));

        // GET: Home
        WorldOfDiscsEntities db = new WorldOfDiscsEntities();
        public ActionResult Index()
        {
            #region Ghi log
            log.Debug("Debug message");
            log.Warn("Warn message");
            log.Error("Error message");
            log.Fatal("Fatal message");
            #endregion
            return View(db.Discs.Where(n=>n.IsNew==1).ToList());
        }
    }
}