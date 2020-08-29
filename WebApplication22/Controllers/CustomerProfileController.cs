using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication22.Models;

namespace WebApplication22.Controllers
{
    public class CustomerProfileController : Controller
    {
        //
        // GET: /CustomerProfile/
        private projectdb db = new projectdb();
        public ActionResult CustomerProfile(String data , String id)
        {
            if (data == null || id == null)
            {
                data = WelcomeController.Tempstatus;
                id = WelcomeController.Tempid;
            }
            if (data != null && id != null) {
             TempData["status"] = WelcomeController.Tempstatus;
             TempData["id"] = WelcomeController.Tempid;
            }
            int i = Int32.Parse(WelcomeController.Tempid);
           // var name =  db.usertable.Single(u => u.userid == i);
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == i select new viewmodel { u = e };
            ViewData["info"] = x.ToList();
            var name = from e  in db.usertable join p in db.projecttable on e.userid equals p.Customer.userid where e.userid==i & p.Manager.userid==null select new viewmodel{u=e ,p=p};
            return View(name);
        }

        public ActionResult DeliveredTable()
        {
            int i = Int32.Parse(WelcomeController.Tempid);
            var x = from e in db.projecttable join p in db.usertable on e.Customer.userid equals p.userid where e.Customer.userid == i select new viewmodel { p = e, u = p };
            var xx = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == i select new viewmodel { u = e };
            ViewData["info"] = xx.ToList();
            return View(x);
        }
        [HttpGet]
        public ActionResult ProfilePostProject()
        {
            int i = Int32.Parse(WelcomeController.Tempid);
            TempData["id"] = WelcomeController.Tempid;
            var name = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == i select new viewmodel { u = e };
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == i select new viewmodel { u = e };
            ViewData["info"] = x.ToList();
            return View(name);
        }
        [HttpPost]
        public ActionResult ProfilePostProject(String describtion, String projectname)
        {
            project p = new project();
            user u = db.usertable.Find(Int32.Parse(WelcomeController.Tempid));
            p.Customer = u;
            p.describtion = describtion;
            p.projectname = projectname;
            p.status = "unsigned";
            db.projecttable.Add(p);
            db.SaveChanges();
            return RedirectToAction("../Home/Index");
        }
        public ActionResult MTCurrentProjects()
        {
            int i = Int32.Parse(WelcomeController.Tempid);
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == i select new viewmodel { u = e };
            ViewData["info"] = x.ToList();
            var y= from e in db.projecttable join p in db.worksontable on e.projectid equals p.workignproject.projectid join u in db.usertable on e.Customer.userid equals u.userid where p.trainee.userid == i & e.status != "finished" select new viewmodel { p = e,u=u };
            return View(y);
        }
        public ActionResult Deleteproject(int id)
        {
            var x = db.projecttable.Find(id);
            int i = Int32.Parse(WelcomeController.Tempid);
            try
            {
                userrequest z = db.urequesttable.Single(e => e.sender.userid == i & e.projects.projectid == x.projectid);
                    db.urequesttable.Remove(z);
                    db.SaveChanges();
                    db.projecttable.Remove(x);
                    db.SaveChanges();
                

            }
            catch
            {
                db.projecttable.Remove(x);
                db.SaveChanges();
            }
           
            return RedirectToAction("CustomerProfile");
        }
        public ActionResult MTQualifications()
        {
            int i = Int32.Parse(WelcomeController.Tempid);
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == i select new viewmodel { u = e };
            ViewData["info"] = x.ToList();
            var finshiedproject = from e in db.worksontable join p in db.projecttable on e.workignproject.projectid equals p.projectid where p.status == "finished" & e.trainee.userid == i select e;
            var wokringprojects = from e in db.worksontable join p in db.projecttable on e.workignproject.projectid equals p.projectid where p.status != "finished" & e.trainee.userid == i select e;
            var feedbacks = from e in db.feedbacktable where e.Trainee.userid == i select e;



            int count = 0;
            int count1 = 0;
            int count2 = 0;
            int count3 = 0;
            foreach (var counter in finshiedproject.ToList())
            {
                count++;
            }
            foreach (var counter in wokringprojects.ToList())
            {
                count1++;
            }
            count2 = count1 + count;
            foreach (var counter in feedbacks.ToList())
            {
                count3++;
            }
            ViewData["allprojects"] = count2;
            ViewData["finishprojects"] = count;
            ViewData["workingprojects"] = count1;
            ViewData["feedbacks"] = count3;

            return View();
        }
        public ActionResult MTHistory()
        {


            int i = Int32.Parse(WelcomeController.Tempid);
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == i select new viewmodel { u = e };
            ViewData["info"] = x.ToList();
            var y = from e in db.projecttable join p in db.worksontable on e.projectid equals p.workignproject.projectid join u in db.usertable on e.Customer.userid equals u.userid where p.trainee.userid == i & e.status == "finished" select new viewmodel { p = e ,u=u};
            return View(y);
        }
        [HttpGet]
        public ActionResult EditNotDelivered(int id)
        {
            int i = Int32.Parse(WelcomeController.Tempid);
            TempData["id"] = WelcomeController.Tempid;
            var name = from e in db.usertable join p in db.projecttable on e.userid equals p.Customer.userid where e.userid == i & p.projectid==id select new viewmodel { u = e,p=p };
            var x = from ee in db.usertable join p in db.usertable on ee.userid equals p.userid where ee.status == "Marketing Director" select new viewmodel { u = ee };
            ViewData["data"] = x.ToList();
            var xx = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == i select new viewmodel { u = e };
            ViewData["info"] = xx.ToList();

            return View(name);
        }
        [HttpPost]
        public ActionResult EditNotDelivered(int id, String describtion, String projectname)
        {
            int i = Int32.Parse(WelcomeController.Tempid);
            project x = db.projecttable.Find(id);
            x.describtion = describtion;
            x.projectname = projectname;
            if (TryUpdateModel(x))
            {
                db.SaveChanges();
            }



           
            return RedirectToAction("CustomerProfile");
            }
        public ActionResult Leave(int id)
        {
            project p = db.projecttable.Find(id);
            db.Entry(p).Reference("Manager").Load();
            user manager = db.usertable.Find(p.Manager.userid);
            int i = Int32.Parse(WelcomeController.Tempid);
            user trainee = db.usertable.Find(i);
            userrequest r = new userrequest();
            r.projects = p;
            r.reciver = manager;
            r.sender = trainee;
            r.satuts = "non seen";
            r.request = "can i leave this project for some personal reasons";
            db.urequesttable.Add(r);
            db.SaveChanges();
            return RedirectToAction("CustomerProfile");

        }
        public ActionResult MTRequests()
        {
            int i = Int32.Parse(WelcomeController.Tempid);
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == i select new viewmodel { u = e };
            ViewData["info"] = x.ToList();
            var request = from e in db.drrequesttable join p in db.projecttable on e.projects.projectid equals p.projectid join u in db.usertable on p.Customer.userid equals u.userid where e.reciver.userid == i & e.status == "not seen" select new viewmodel { d = e, p = p, u = u };
            var mrequest = from e in db.urequesttable join p in db.projecttable on e.projects.projectid equals p.projectid join u in db.usertable on p.Customer.userid equals u.userid where e.reciver.userid == i & e.satuts == "not seen" select new viewmodel { r = e, p = p, u = u };
            var list1 = request.ToList();
            var list2 = mrequest.ToList();
            var list3 = list1.Concat(list2).ToList();
            return View(list3);
        }
        public ActionResult Acceptrequest(int id , int pid)
        {
            int i = Int32.Parse(WelcomeController.Tempid);
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == i select new viewmodel { u = e };
            ViewData["info"] = x.ToList();
            workson w = new workson();
            user trainee = db.usertable.Find(i);
            project pro  = db.projecttable.Find(pid);
            w.trainee = trainee;
            w.workignproject = pro;
            db.worksontable.Add(w);
            db.SaveChanges();
            drrequest d = db.drrequesttable.Find(id);
            d.status = "accept";
            db.SaveChanges();
            return RedirectToAction("MTRequests");
        }
        public ActionResult Refuserequest(int id)
        {
            int i = Int32.Parse(WelcomeController.Tempid);
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == i select new viewmodel { u = e };
            ViewData["info"] = x.ToList();
            drrequest d = db.drrequesttable.Find(id);
            d.status = "refuse";
            db.SaveChanges();
            return RedirectToAction("MTRequests");
        }
        public ActionResult CustomerRequests(int id)
        {


            int i = Int32.Parse(WelcomeController.Tempid);
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == i select new viewmodel { u = e };
            ViewData["info"] = x.ToList();
            int ooo = (int)Session["id"];
            var request = from e in db.drrequesttable join p in db.usertable on e.Sender.userid equals p.userid where e.reciver.userid == ooo & e.status == "non seen" & e.projects.projectid == id select new viewmodel { d = e, u = p };
            return View(request);
        }
        public ActionResult Accept(int drid)
        {
            int ooo = (int)Session["id"];
            drrequest d = db.drrequesttable.Find(drid);
            db.Entry(d).Reference("Sender").Load();
            db.Entry(d).Reference("projects").Load();
            user u = db.usertable.Find(d.Sender.userid);
            project p = db.projecttable.Find(d.projects.projectid);
            d.status = "Accept";
            db.SaveChanges();
            p.startdate = d.startdate;
            p.enddate = d.enddate;
            p.price = d.price;
            db.Entry(p).Reference("Manager").CurrentValue = u;
            p.status = "signed";
            db.SaveChanges();
            var request = from e in db.drrequesttable where e.projects.projectid == d.projects.projectid & e.status == "non seen" select e;
            foreach (var k in request.ToList())
            {
                db.drrequesttable.Remove(k);
                db.SaveChanges();
            }

            return RedirectToAction("DeliveredTable");
        }
        public ActionResult Refuse(int drid)
        {
            drrequest d = db.drrequesttable.Find(drid);
            d.status = "Refuse";
            db.SaveChanges();

            return RedirectToAction("DeliveredTable");
        }
        }
	}
