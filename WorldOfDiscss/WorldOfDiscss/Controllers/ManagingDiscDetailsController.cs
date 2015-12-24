using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorldOfDiscs.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
using log4net;

namespace WorldOfDiscs.Controllers
{
    public class ManagingDiscDetailsController : Controller
    {
        //cấu hình ghi log 
        private static log4net.ILog Log { get; set; }
        ILog log = log4net.LogManager.GetLogger(typeof(ForumController));
        WorldOfDiscsEntities db = new WorldOfDiscsEntities();

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.GroupCategory = new SelectList(db.Group_Category.ToList(), "Id", "Name");
            ViewBag.Category = new SelectList(db.Categories.ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Disc disc, FormCollection f, HttpPostedFileBase small, HttpPostedFileBase big)
        {
            #region Luu imamge
            try
            {
                //lưu tên file avatar
                var smallName = Path.GetFileName(small.FileName);
                //lưu đường dẫn
                var path = Path.Combine(Server.MapPath("~/img/disc/"), smallName);
                if (System.IO.File.Exists(path))
                {
                }
                else
                {
                    small.SaveAs(path);
                }
                disc.Image_small = smallName;
            }
            catch (Exception ex)
            {
                #region Ghi log
                log.Error(ex);
                #endregion
            }
            try
            {
                //lưu tên file avatar
                var bigName = Path.GetFileName(big.FileName);
                //lưu đường dẫn
                var path = Path.Combine(Server.MapPath("~/img/disc/"), bigName);
                if (System.IO.File.Exists(path))
                {
                }
                else
                {
                    big.SaveAs(path);
                }
                disc.Image_big = bigName;
            }
            catch (Exception ex)
            {
                #region Ghi log
                log.Error(ex);
                #endregion
            }
            #endregion

            int Id_GroupCategory = Int32.Parse((f.Get("GroupCategory").ToString()));
            int Id_Category = Int32.Parse((f.Get("Category").ToString()));
            disc.Id_Category = Id_Category;
            disc.Id_Group_Category = Id_GroupCategory;

            if(f.Get("IsNew") != null)
            { disc.IsNew = 1;}
            else
            { disc.IsNew = 0; }

            db.Discs.Add(disc);
            db.SaveChanges();
            ViewBag.Success = "Disc successfully created !";
            ViewBag.Error = "";
            ViewBag.GroupCategory = new SelectList(db.Group_Category.ToList(), "Id", "Name");
            ViewBag.Category = new SelectList(db.Categories.ToList(), "Id", "Name");
            return View();
        }

        
    }

}