using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication22.Models;

namespace WebApplication22.Controllers
{
    public class HomeController : Controller
    {
        public projectdb db = new projectdb();
            //
        // GET: /Home/
        [HttpGet]
        public ActionResult index(String data,String id)
        {
            if (data == null || id == null)
            {
                data = WelcomeController.Tempstatus;
                id = WelcomeController.Tempid;

            }
            if (data != null && id != null)
            {
                TempData["status"] = data;
                WelcomeController.Tempstatus = data;
                TempData["status"   ] = WelcomeController.Tempstatus;
                TempData["id"] = WelcomeController.Tempid;
            }
            int i = Int32.Parse(WelcomeController.Tempid);
            user name = db.usertable.Single(u => u.userid == i);
            TempData["name"] = name.firstname;
            ViewData["userphoto"] = name.photo;
            var xx = from ee in db.usertable join p in db.usertable on ee.userid equals p.userid where ee.status == "Marketing Director" select new viewmodel { u = ee };
            ViewData["data"] = xx.ToList();
            String check = Session["status"].ToString();
            int ooo = (int)Session["id"];
            var x = from e in db.projecttable join p in db.usertable on e.Customer.userid equals p.userid where e.Manager.userid == null orderby e.projectid select new viewmodel { p = e, u = p };
            var y = from e in db.drrequesttable join p in db.projecttable on e.projects.projectid equals p.projectid join u in db.usertable on p.Customer.userid equals u.userid where e.Sender.userid == ooo & e.status != "Refuse" select new viewmodel { p = p, u = u };
            var list1 = x.ToList();
            var list2 = y.ToList();
            var list3 = list1.Where(p => !list2.Any(p2 => p.u == p2.u));
            return View(list3);
        }
        [HttpPost]
        public ActionResult index(String describtion, String projectname,String pramter)
        {
            int i = Int32.Parse(WelcomeController.Tempid);
                project p = new project();
                user u = db.usertable.Find(Int32.Parse(WelcomeController.Tempid));
                p.Customer = u;
                p.describtion = describtion;
                p.projectname = projectname;
                p.status = "unsigned";
                db.projecttable.Add(p);
                db.SaveChanges();
            
           
            return RedirectToAction("Index");
        }
        public ActionResult aboutUs()
        {
            int i = Int32.Parse(WelcomeController.Tempid);
            user name = db.usertable.Single(u => u.userid == i);
            TempData["name"] = name.firstname;
            return View();
        }
        public ActionResult contactUs()
        {
            int i = Int32.Parse(WelcomeController.Tempid);
            user name = db.usertable.Single(u => u.userid == i);
            TempData["name"] = name.firstname;
            return View();
        }
        [HttpPost]
        public ActionResult Mdsign(String startday, String startmonth, String startyear, String endday, String endmonth, String endyear,int price,int id)
        {
            int ooo = (int)Session["id"];
            String startdate = startday + "/" + startmonth + "/" + startyear;
            String enddate = endday + "/" + endmonth + "/" + endyear;
            project p = db.projecttable.Find(id);
            db.Entry(p).Reference("Customer").Load();
            user u = db.usertable.Find(ooo);
            user c = db.usertable.Find(p.Customer.userid);    
            drrequest dr = new drrequest();
            dr.Sender = u;
            dr.projects = p;
            dr.reciver = c;
            dr.status = "non seen";
            dr.request = "can you accept my request to sign this project";
            dr.price = price;
            dr.startdate = startdate;
            dr.enddate = enddate;
            db.drrequesttable.Add(dr);
            db.SaveChanges();

            return RedirectToAction("index");
        }
      
     }
}   