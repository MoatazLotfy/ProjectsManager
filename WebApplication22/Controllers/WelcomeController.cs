using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication22.Models;
using System.IO;
namespace WebApplication22.Controllers
{
    public class WelcomeController : Controller
    {
        public static String Tempstatus;
        public static String Tempid;
        public projectdb db = new projectdb();
        [HttpGet]
        public ActionResult Welcome()
        {
            
            return View();
        }
        //
        // GET: /Welcom/
        [HttpGet]
        public ActionResult LoginView()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult LoginView(String email, String password)
        {
            try
            {

                var id = db.usertable.Single(u => u.Email == email & u.password == password);
                TempData["status"] = id.status;
                TempData["id"] = id.userid.ToString();
                Tempid = id.userid.ToString();
                Tempstatus = id.status;
                TempData["status"] = Tempstatus;
                TempData["id"] = Tempid.ToString();
                Session["id"] = id.userid;
                Session["status"] = id.status;
                var sum = "true";
                return Json(new { sum });
            }
            catch (Exception e)
            {

                return Json(new { email });
            }
        }
          [HttpGet]
        public ActionResult register()
        {
            
           
            return View();
        }
        [HttpPost]
        public ActionResult register(user userr,HttpPostedFileBase photo)
        {
            ViewBag.name = "_Register"; 
            try
            {
                user u = db.usertable.Single(e => e.Email == userr.Email);
                return View();
            }
            catch
            {
                userr.photo = photo.FileName;
                db.usertable.Add(userr);
                db.SaveChanges();
                if(photo!=null)
                {
                    string path = Path.Combine(Server.MapPath("~/userimages"), Path.GetFileName(photo.FileName));
                    photo.SaveAs(path);
                    user u = db.usertable.Single(e => e.Email == userr.Email);
                    TempData["status"] = u.status;
                    TempData["id"] = u.userid.ToString();
                    Tempid = u.userid.ToString();
                    Tempstatus = u.status;
                    TempData["status"] = Tempstatus;
                    TempData["id"] = Tempid.ToString();
                    Session["id"] = u.userid;
                    Session["status"] = u.status;
                    return RedirectToAction("../Home/index");
                }
                return View();
               
                
            }
      
        }
	}
}