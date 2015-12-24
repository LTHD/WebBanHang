using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorldOfDiscs.Models;
using PagedList;
using PagedList.Mvc;

namespace WorldOfDiscs.Controllers
{
    public class ManagingDiscCategoriesController : Controller
    {
        WorldOfDiscsEntities db = new WorldOfDiscsEntities();

        public ActionResult CreateGC(FormCollection f)
        {
            Group_Category gc = new Group_Category();
            gc.Name = f.Get("txtName").ToString();
            db.Group_Category.Add(gc);
            db.SaveChanges();
            return RedirectToAction("ManagingDiscCategories", "Admin");
        }

        public ActionResult CreateC(FormCollection f)
        {
            Category c = new Category();
            c.Name = f.Get("txtName").ToString();
            int Id_GroupCategory = Int32.Parse((f.Get("GroupCategory").ToString()));
            c.Id_Group_Category = Id_GroupCategory;
            db.Categories.Add(c);
            db.SaveChanges();
            return RedirectToAction("ManagingDiscCategories", "Admin");
        }

        public ActionResult DeleteGC(int Id_GC)
        {
            try
            {
                Group_Category us = db.Group_Category.SingleOrDefault(n => n.Id == Id_GC);
                if (us == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                db.Group_Category.Remove(us);
                db.SaveChanges();
                Session["ErrorGC"] = null;
                Session["SuccessGC"] = "The group category was deleted successfully !";
                return RedirectToAction("ManagingDiscCategories", "Admin");
            }
            catch (Exception)
            {
                Session["ErrorGC"] = "Unable to delete the group category !";
                Session["SuccessGC"] = null;
                return RedirectToAction("ManagingDiscCategories", "Admin");
            }
        }

        public ActionResult DeleteC(int Id_Category)
        {
            try
            {
                Category us = db.Categories.SingleOrDefault(n => n.Id == Id_Category);
                if (us == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                db.Categories.Remove(us);
                db.SaveChanges();
                Session["ErrorC"] = null;
                Session["SuccessC"] = "The category was deleted successfully !";
                return RedirectToAction("ManagingDiscCategories", "Admin");
            }
            catch (Exception)
            {
                Session["ErrorC"] = "Unable to delete the category !";
                Session["SuccessC"] = null;
                return RedirectToAction("ManagingDiscCategories", "Admin");
            }
        }

        
    }
}