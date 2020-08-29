using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication22.Models;
using System.IO;


namespace WebApplication22.Controllers
{
    public class MDProfileController : Controller
    {
        public projectdb db = new projectdb();
        public static int proid;
        public static int train;
        
        //
        // GET: /MDProfile/
        [HttpGet]
        public ActionResult MDProfile()
        {
            int ooo = (int)Session["id"];
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == ooo select new viewmodel { u = e };
            ViewData["mdinfo"] = x.ToList();
            var pro = from e in db.projecttable join p in db.usertable on e.Customer.userid equals p.userid where e.Manager.userid == ooo & e.status !="finished" select new viewmodel { p = e,u=p };
            return View(pro);
        }
        [HttpGet]
        public ActionResult MDAddTeamleader(int pid)
        {
           proid = pid;
           int ooo = (int)Session["id"];
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == ooo select new viewmodel { u = e };
            ViewData["mdinfo"] = x.ToList();
            var y = from e in db.usertable join p in db.projecttable on e.userid equals p.leader.userid where p.projectid == pid & p.leader != null select new viewmodel { u = e };
            var teamleaders = from t in db.usertable join s in db.usertable on t.userid equals s.userid where t.status == "Marketing team leader" select new viewmodel { u = t };
            var rr = from e in db.drrequesttable join p in db.usertable on e.reciver.userid equals p.userid where e.reciver.status == "Marketing team leader" & e.projects.projectid == proid & e.status != "Refuse" select new viewmodel { u = p };
            var list1 = y.ToList();
            var list2 = teamleaders.ToList();
            if(y.ToList().Count!=0)
            {
                
                var list3 = list2.Where(p => !list2.Any(p2 => p.u == p2.u));
                return View(list3);

            }
            else
            {
                var list3 = rr.ToList();
                var list4 = list2.Where(p => !list3.Any(p2 => p.u == p2.u));
                return View(list4);
                
            }
           
        }
        
        public ActionResult RLeader(String email)
        {
            var p = db.projecttable.Find(proid);
            var x = db.usertable.Single(e => e.Email==email);
            int ooo = (int)Session["id"];
            var u = db.usertable.Find(ooo);
            try {
                var check = db.drrequesttable.Single(e => e.projects.projectid == proid & e.reciver.userid == x.userid);
                db.Entry(check).Reference("reciver").CurrentValue = x;
                db.SaveChanges();

            }
            catch
            {
                drrequest r = new drrequest();
                r.projects = p;
                r.Sender = u;
                r.reciver = x;
                r.request = "can you join me this project";
                r.status = "non seen";
                db.drrequesttable.Add(r);
                db.SaveChanges();
            }
            
            
            return RedirectToAction("MDProfile");

        }
        [HttpGet]
        public ActionResult MDAddTrainee(int pid)
        {
            proid = pid;
            int ooo = (int)Session["id"];
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == ooo select new viewmodel { u = e };
            ViewData["mdinfo"] = x.ToList();
            var worker = from e in db.worksontable join p in db.usertable on e.trainee.userid equals p.userid where e.workignproject.projectid == pid select new viewmodel {u=p };
            var trainee = from t in db.usertable join s in db.usertable on t.userid equals s.userid where t.status == "Trainee" select new viewmodel { u = t };
            var r = from e in db.urequesttable join p in db.usertable on e.reciver.userid equals p.userid where e.reciver.status == "Trainee"  &e.projects.projectid == proid & e.satuts != "Refuse" select new viewmodel { u = p };
            var rr = from e in db.drrequesttable join p in db.usertable on e.reciver.userid equals p.userid where e.reciver.status == "Trainee" & e.projects.projectid == proid & e.status != "Refuse" select new viewmodel { u = p };
            var list1 = worker.ToList();
            var list2 = trainee.ToList();
            var list4 = r.ToList();
            var list6 = rr.ToList();
            var list3 = list2.Where(p => !list1.Any(p2=> p.u == p2.u));
            var list5 = list3.Where(p => !list4.Any(p2 => p.u == p2.u));
            var list7 = list5.Where(p => !list6.Any(p2 => p.u == p2.u));
            return View(list7);
        }
        [HttpPost]
        public ActionResult MDAddTrainee(int pid, String[] train)
        {
            var p = db.projecttable.Find(proid);
            int ooo = (int)Session["id"];
            var u = db.usertable.Find(ooo);
            
                foreach (var x in train)
                {

                    var t = db.usertable.Single(e => e.Email == x);
                    drrequest r = new drrequest();
                    r.projects = p;
                    r.Sender = u;
                    r.reciver = t;
                    r.request = "can you join me this project";
                    r.status = "not seen";
                    db.drrequesttable.Add(r);
                    db.SaveChanges();

                
            }
            return RedirectToAction("MDProfile");

        }
        [HttpGet]
        public ActionResult MDControlProject(int pid )

        {
            proid = pid;
            int ooo = (int)Session["id"];
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == ooo select new viewmodel { u = e };
            ViewData["mdinfo"] = x.ToList();
            var project = from e in db.projecttable join p in db.projecttable on e.projectid equals p.projectid where e.projectid == pid select new viewmodel { p = e };
           
            return View(project);
        }
        [HttpPost]
        public ActionResult MDControlProject(String startday, String startmonth, String startyear, String endday, String endmonth, String endyear, String price, String status)
        {
             int ooo = (int)Session["id"];
            String startdate = startday+"/"+startmonth+"/"+startyear;
            String enddate = endday + "/" + endmonth + "/" + endyear;
           
            
                project p = db.projecttable.Find(proid);
                p.status = status;
                db.SaveChanges();
                db.Entry(p).Reference("Customer").Load();
                user Customer = db.usertable.Find(p.Customer.userid);
                user Manager = db.usertable.Find(ooo);
                drrequest r = new drrequest();
                r.price = Int32.Parse(price);
                r.Sender = Manager;
                r.reciver = Customer;
                r.request = "can you accept my change";
                r.status = "not seen";
                r.startdate = startdate;
                r.enddate = enddate;
                r.projects = p;
                db.drrequesttable.Add(r);
                db.SaveChanges();
            


            return RedirectToAction("MDProfile");
        }
           public ActionResult MDDeleteMember()
        {
            int ooo = (int)Session["id"];
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == ooo select new viewmodel { u = e };
          ViewData["mdinfo"] = x.ToList();
          var rr = from e in db.worksontable join p in db.projecttable on e.workignproject.projectid equals p.projectid join u in db.usertable on e.trainee.userid equals u.userid where e.workignproject.projectid == proid | p.leader != null select new viewmodel { w = e, p = p, u = u };
          var rrr = from e in db.worksontable join p in db.projecttable on e.workignproject.projectid equals p.projectid join u in db.usertable on p.leader.userid equals u.userid where e.workignproject.projectid == proid | p.leader != null select new viewmodel { w = e, p = p, u = u };
          var list1 = rr.ToList();
          var list2 = rrr.ToList();
          var list3 = list1.Concat(list2).ToList();
          return View(list3);
         
        }
        public ActionResult Dmember(int id)
        {
            user u = db.usertable.Find(id);
            if (u.status.Equals("Marketing team leader"))
            {
                project p = db.projecttable.Find(proid);
                db.Entry(p).Reference("leader").CurrentValue = null;
                db.SaveChanges();
            }
            else
            {
                workson x = db.worksontable.Single(e => e.trainee.userid == id & e.workignproject.projectid == proid);
                db.worksontable.Remove(x);
                db.SaveChanges();

            }

            return RedirectToAction("MDDeleteMember");
        }
        public ActionResult Dteamleader()
        {
            project p = db.projecttable.Find(proid);
            db.Entry(p).Reference("leader").CurrentValue = null;
            db.SaveChanges();
            return RedirectToAction("MDDeleteMember");
        }
        public ActionResult MDProjectDescription(int pid)
        {
            int ooo = (int)Session["id"];
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == ooo select new viewmodel { u = e };
            ViewData["mdinfo"] = x.ToList();
            var project = from e in db.projecttable join p in db.projecttable on e.projectid equals p.projectid where e.projectid == pid select new viewmodel { p = e };
           
            return View(project);
        }
        public ActionResult MDHistory()
        {
            int ooo = (int)Session["id"];
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == ooo select new viewmodel { u = e };
            ViewData["mdinfo"] = x.ToList();
            var projects = from e in db.projecttable join p in db.usertable on e.Customer.userid equals p.userid where e.status=="finished" & e.Manager.userid == ooo select new viewmodel { p = e ,u=p };

           
            return View(projects);
        }   
        public ActionResult MDQualifications()
        {
            int ooo = (int)Session["id"];
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == ooo  select new viewmodel { u = e };
            ViewData["mdinfo"] = x.ToList();
            var finshiedproject = from e in db.projecttable where e.Manager.userid == ooo & e.status == "finished" select e;
            var wokringprojects = from e in db.projecttable where e.Manager.userid == ooo & e.status != "finished" select e;
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

            ViewData["allprojects"] = count2;
            ViewData["finishprojects"] = count;
            ViewData["workingprojects"] = count1;
            ViewData["feedbacks"] = count3;
            return View();
        }
        public ActionResult Leaveproject()
        {
            project p = db.projecttable.Find(proid);
            db.Entry(p).Reference("Manager").CurrentValue = null;
            db.Entry(p).Reference("leader").CurrentValue = null;
            p.price = 0;
            p.startdate = null;
            p.enddate = null;
            p.status = "unsigned";
            db.SaveChanges();
            try
            {
                var d = from e in db.drrequesttable where e.projects.projectid==proid select e;
                foreach(drrequest x in d.ToList())
                {
                    db.drrequesttable.Remove(x);
                    db.SaveChanges();


                }
            }
            catch
            {
            }
            try
            {
                var w = from e in db.worksontable where e.workignproject.projectid == proid select e;
                foreach (workson x in w.ToList())
                {
                    db.worksontable.Remove(x);
                    db.SaveChanges();


                }
            }
            catch
            {

            }

            return RedirectToAction("MDProfile");

        }
        public ActionResult AdminProfileControlPost()
        {
            int ooo = (int)Session["id"];
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == ooo select new viewmodel { u = e };
            ViewData["mdinfo"] = x.ToList();
            var projects = from e in db.projecttable join p in db.usertable on e.Customer.userid equals p.userid where e.status != "finished" select new viewmodel { p = e, u=p};
            return View(projects);
        }
        [HttpGet]
        public ActionResult AdminControlPost(int pid)
        {
            int ooo = (int)Session["id"];
            proid = pid;
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == ooo select new viewmodel { u = e };
            ViewData["mdinfo"] = x.ToList();
            var projects = from e in db.projecttable join p in db.usertable on e.Customer.userid equals p.userid where e.projectid == pid select new viewmodel{ p = e, u = p };
            var MD = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.status == "Marketing Director" select new viewmodel { u = e };
            ViewData["MD"] = MD.ToList();
            return View(projects);
        }
        [HttpPost]
        public ActionResult AdminControlPost(int pid, String describtion, String projectname)
        {
            int ooo = (int)Session["id"];
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == ooo select new viewmodel { u = e };
            ViewData["mdinfo"] = x.ToList();
            var project = db.projecttable.Find(pid);
            db.Entry(project).Reference("Customer").Load();
            proid = pid;
            project.describtion = describtion;
            project.projectname = projectname;
            db.SaveChanges();
            
            return RedirectToAction("AdminProfileControlPost");
        }
        public ActionResult Removep()
        {
            var urquest = from e in db.urequesttable where  e.projects.projectid == proid select e;
            foreach (var k in urquest.ToList())
            {
                db.urequesttable.Remove(k);
                db.SaveChanges();
            }
            var dr = from e in db.drrequesttable where e.projects.projectid == proid select e;
            foreach (var k in dr.ToList())
            {
                db.drrequesttable.Remove(k);
                db.SaveChanges();
            }
            var workson = from e in db.worksontable where e.workignproject.projectid == proid select e;
            foreach (var k in workson.ToList())
            {
                db.worksontable.Remove(k);
                db.SaveChanges();
            }
            var feedback = from e in db.feedbacktable where e.project.projectid == proid select e;
            foreach (var k in feedback.ToList())
            {
                db.feedbacktable.Remove(k);
                db.SaveChanges();
            }
            project pro = db.projecttable.Find(proid);
            db.projecttable.Remove(pro);
            db.SaveChanges();
            return RedirectToAction("AdminProfileControlPost");

        }
        [HttpGet]
        public ActionResult AdminCreatePost()
        {
            int ooo = (int)Session["id"];
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == ooo select new viewmodel { u = e };
            ViewData["mdinfo"] = x.ToList();
            var users = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.status == "Client" select new viewmodel { u = e };
            ViewData["users"] = users.ToList();
            var MD = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.status == "Marketing Director" select new viewmodel { u = e };
            return View(MD);
        }
        [HttpPost]
        public ActionResult AdminCreatePost(String projectname, String describtion, String list, String user)
        {
            int ooo = (int)Session["id"];
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == ooo select new viewmodel { u = e };
            ViewData["mdinfo"] = x.ToList();
            project po = new project();
            user customer = db.usertable.Single(e => e.Email == user);
            if (!list.Equals("Select MD"))
            {
                user Manager = db.usertable.Single(e => e.Email == list);
                po.describtion=describtion;
                po.projectname=projectname;
                po.Manager=Manager;
                po.Customer=customer;
                po.status = "signed";
                db.projecttable.Add(po);
                db.SaveChanges();
            }
            else
            {
                po.describtion = describtion;
                po.projectname = projectname;
                po.Customer = customer;
                po.status = "unsigned";
                db.projecttable.Add(po);
                db.SaveChanges();
            }


            return RedirectToAction("AdminProfileControlPost");
        }
        public ActionResult AdminControltrainee()
        {
            int ooo = (int)Session["id"];
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == ooo select new viewmodel { u = e };
            ViewData["mdinfo"] = x.ToList();
            var trainee = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.status == "Trainee" select new viewmodel { u = e };
            return View(trainee);
        }
        public ActionResult controlltrainee(int id)
        {
            var workson = from e in db.worksontable where e.trainee.userid==id select e;
            foreach(var x in workson.ToList())
            {
                db.worksontable.Remove(x);
                db.SaveChanges();
            }
            var urequests = from e in db.urequesttable where e.sender.userid == id | e.reciver.userid == id select e;
            foreach (var x in urequests.ToList())
            {
                db.urequesttable.Remove(x);
                db.SaveChanges();
            }
            var drequest = from e in db.drrequesttable where e.Sender.userid == id | e.reciver.userid == id select e;
            foreach (var x in drequest.ToList())
            {
                db.drrequesttable.Remove(x);
                db.SaveChanges();
            }
            var trainee = from e in db.feedbacktable where e.Trainee.userid == id select e;
            foreach (var x in trainee.ToList())
            {
                db.feedbacktable.Remove(x);
                db.SaveChanges();
            }
            user user = db.usertable.Find(id);
            db.usertable.Remove(user);
            db.SaveChanges();

            return RedirectToAction("AdminControltrainee");
        }
        public ActionResult AdminControlcustomer()
        {
            int ooo = (int)Session["id"];
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == ooo select new viewmodel { u = e };
            ViewData["mdinfo"] = x.ToList();
            var customer = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.status == "Client" select new viewmodel { u = e };
            return View(customer);
        }
        public ActionResult controlluser(int id)
        {
            var urequest = from e in db.urequesttable where e.reciver.userid == id | e.sender.userid == id select e;
            foreach(var x in urequest.ToList())
            {
                db.urequesttable.Remove(x);
                db.SaveChanges();
            }
            var drequest = from e in db.drrequesttable where e.reciver.userid == id | e.Sender.userid == id select e;
            foreach(var x in drequest.ToList())
            {
                db.drrequesttable.Remove(x);
                db.SaveChanges();
            }
            var projects = from e in db.projecttable where e.Customer.userid == id & e.status != "finished" select e;
            foreach (var x in projects.ToList())
            {
                db.projecttable.Remove(x);
                db.SaveChanges();
            }

            var stayp = from e in db.projecttable where e.Customer.userid == id & e.status == "finished" select e;
            foreach(var x in stayp.ToList())
            {
                db.Entry(x).Reference("Customer").CurrentValue = null;
                db.SaveChanges();
            }
            user user = db.usertable.Find(id);
            db.usertable.Remove(user);
            db.SaveChanges();
            return RedirectToAction("AdminControlcustomer");
        }
        public ActionResult AdminControlteamleader()
        {
            int ooo = (int)Session["id"];
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == ooo select new viewmodel { u = e };
            ViewData["mdinfo"] = x.ToList();
            var teamleader = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.status == "Marketing team leader" select new viewmodel { u = e };
            
            return View(teamleader);
        }
        public ActionResult controllleader (int id)
        {
            var urequest = from e in db.urequesttable where e.reciver.userid == id | e.sender.userid == id select e;
            foreach (var x in urequest.ToList())
            {
                db.urequesttable.Remove(x);
                db.SaveChanges();
            }
            var drequest = from e in db.drrequesttable where e.reciver.userid == id | e.Sender.userid == id select e;
            foreach (var x in drequest.ToList())
            {
                db.drrequesttable.Remove(x);
                db.SaveChanges();
            }
            var projects = from e in db.projecttable where e.leader.userid == id select e;
            foreach (var x in projects.ToList())
            {
                db.Entry(x).Reference("leader").CurrentValue = null;
                db.SaveChanges();

            }
            var teamleader = from e in db.feedbacktable where e.Sender.userid == id select e;
            foreach (var x in teamleader.ToList())
            {
                db.feedbacktable.Remove(x);
                db.SaveChanges();

            }
            user user = db.usertable.Find(id);
            db.usertable.Remove(user);
            db.SaveChanges();

            return RedirectToAction("AdminControlteamleader");
        }
        public ActionResult AdminControlMD()
        {
            int ooo = (int)Session["id"];
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == ooo select new viewmodel { u = e };
            ViewData["mdinfo"] = x.ToList();
            var MD = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.status == "Marketing Director" select new viewmodel { u = e };
            return View(MD);
        }
        public ActionResult controllmd(int id)
        {
            var urequest = from e in db.urequesttable where e.reciver.userid == id | e.sender.userid == id select e;
            foreach (var x in urequest.ToList())
            {
                db.urequesttable.Remove(x);
                db.SaveChanges();
            }
            var drequest = from e in db.drrequesttable where e.reciver.userid == id | e.Sender.userid == id select e;
            foreach (var x in drequest.ToList())
            {
                db.drrequesttable.Remove(x);
                db.SaveChanges();
            }
            var projects = from e in db.projecttable where e.Manager.userid == id select e;
            foreach (var x in projects.ToList())
            {
                db.Entry(x).Reference("Manager").CurrentValue = null;
                db.SaveChanges();

            }
            var md = from e in db.feedbacktable where e.Reciver.userid == id select e;
            foreach (var x in md.ToList())
            {
                db.feedbacktable.Remove(x);
                db.SaveChanges();
            }
            user user = db.usertable.Find(id);
            db.usertable.Remove(user);
            db.SaveChanges();

            return RedirectToAction("AdminControlMD");
        }
        [HttpGet]
        public ActionResult AdminAdduser()
        {
            int ooo = (int)Session["id"];
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == ooo select new viewmodel { u = e };
            ViewData["mdinfo"] = x.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult AdminAdduser(user userr, HttpPostedFileBase photo)
        {
            int ooo = (int)Session["id"];
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == ooo select new viewmodel { u = e };
            ViewData["mdinfo"] = x.ToList();
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
                if (photo != null)
                {
                    string path = Path.Combine(Server.MapPath("~/userimages"), Path.GetFileName(photo.FileName));
                    photo.SaveAs(path);
                    return RedirectToAction("AdminControlcustomer");
                    
                }
                return View();
            }
             
        }

        public ActionResult TeamleaderCurrent()
        {
            int ooo = (int)Session["id"];
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == ooo select new viewmodel { u = e };
            ViewData["mdinfo"] = x.ToList();
            var projects = from e in db.projecttable join p in db.usertable on e.Customer.userid equals p.userid where e.leader.userid == ooo & e.status != "finished" select new viewmodel { u = p, p = e };
            return View(projects);
        }
        public ActionResult TeamleaderHistory()
        {
            int ooo = (int)Session["id"];
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == ooo select new viewmodel { u = e };
            ViewData["mdinfo"] = x.ToList();
            var projects = from e in db.projecttable join p in db.usertable on e.Customer.userid equals p.userid where e.leader.userid == ooo & e.status == "finished" select new viewmodel { u = p, p = e };
            return View(projects);
        }
        public ActionResult TeamleaderRequests()
        {
            int ooo = (int)Session["id"];
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == ooo select new viewmodel { u = e };
            ViewData["mdinfo"] = x.ToList();
            var r = from e in db.drrequesttable join p in db.projecttable on e.projects.projectid equals p.projectid join u in db.usertable on p.Customer.userid equals u.userid where e.reciver.userid == ooo & e.status == "non seen" select new viewmodel { u = u, p = p, d = e };
            return View(r);
        }
        public ActionResult TeamleaderAddTrainee(int poid)
        {
            int ooo = (int)Session["id"];
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == ooo select new viewmodel { u = e };
            ViewData["mdinfo"] = x.ToList();
            proid = poid;
            var worker = from e in db.worksontable join p in db.usertable on e.trainee.userid equals p.userid where e.workignproject.projectid == poid select new viewmodel { u = p };
            var trainee = from t in db.usertable join s in db.usertable on t.userid equals s.userid where t.status == "Trainee" select new viewmodel { u = t };
            var r = from e in db.urequesttable join p in db.usertable on e.reciver.userid equals p.userid where e.reciver.status == "Trainee" & e.projects.projectid == poid & e.satuts != "Refuse" select new viewmodel { u = p };
            var rr = from e in db.drrequesttable join p in db.usertable on e.reciver.userid equals p.userid where e.reciver.status == "Trainee" & e.projects.projectid == poid & e.status != "Refuse" select new viewmodel { u = p };
            var list1 = worker.ToList();
            var list2 = trainee.ToList();
            var list4 = r.ToList();
            var list6 = rr.ToList();
            var list3 = list2.Where(p => !list1.Any(p2 => p.u == p2.u));
            var list5 = list3.Where(p => !list4.Any(p2 => p.u == p2.u));
            var list7 = list5.Where(p => !list6.Any(p2 => p.u == p2.u));
            return View(list7);
        }
        public ActionResult RequestTrainee(int id)
        {
            int ooo = (int)Session["id"];
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == ooo select new viewmodel { u = e };
            ViewData["mdinfo"] = x.ToList();
             project pp = db.projecttable.Find(proid);
             user recive = db.usertable.Find(id);
             user sender = db.usertable.Find(ooo);
            try
            {
                userrequest ur = db.urequesttable.Single(e => e.reciver.userid == id & e.sender.userid == ooo & e.projects.projectid == proid);
            }
            catch
            {
                userrequest ur = new userrequest();
                
                ur.projects = pp;
                ur.sender = sender;
                ur.reciver = recive;
                ur.request = "can you sign in my project";
                ur.satuts = "non seen";
                db.urequesttable.Add(ur);
                db.SaveChanges();
            }
            return RedirectToAction("TeamleaderCurrent");
        }
        public ActionResult TeamLeaderEvaluateMT(int poid)
        {
            int ooo = (int)Session["id"];
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == ooo select new viewmodel { u = e };
            ViewData["mdinfo"] = x.ToList();
            proid = poid;
            var t = from e in db.worksontable join p in db.usertable on e.trainee.userid equals p.userid where e.workignproject.projectid == poid select new viewmodel { w = e, u = p };
            return View(t);
        }
        [HttpGet]
        public ActionResult TeamLeaderEvaluation(int tid)
        {
            int ooo = (int)Session["id"];
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == ooo select new viewmodel { u = e };
            ViewData["mdinfo"] = x.ToList();
            train = tid;
            return View();
        }
        [HttpPost]
        public ActionResult TeamLeaderEvaluation(String describtion)
        {
            int ooo = (int)Session["id"];
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == ooo select new viewmodel { u = e };
            ViewData["mdinfo"] = x.ToList();
            project pr = db.projecttable.Find(proid);
            db.Entry(pr).Reference("Manager").Load();
            user sender = db.usertable.Find(ooo);
            user recive = db.usertable.Find(pr.Manager.userid);
            user trainee = db.usertable.Find(train);


            try
            {
                feedback b = db.feedbacktable.Single(e => e.project.projectid == proid & e.Sender.userid == ooo & e.Reciver.userid == pr.Manager.userid);
            }
            catch
            {
                feedback f = new feedback();
                f.project = pr;
                f.Reciver = recive;
                f.Sender = sender;
                f.Trainee = trainee;
                f.describtion = describtion;
                db.feedbacktable.Add(f);
                db.SaveChanges();
            }

            return RedirectToAction("TeamleaderCurrent");
        }
        public ActionResult RemoveTrainee()
        {
            workson w = db.worksontable.Single(e => e.trainee.userid == train);
            db.worksontable.Remove(w);
            db.SaveChanges();
            return RedirectToAction("TeamleaderCurrent");
        }
        public ActionResult AcceptRequest(int poid,int drid)
        {
            int ooo = (int)Session["id"];
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == ooo select new viewmodel { u = e };
            ViewData["mdinfo"] = x.ToList();
            drrequest d = db.drrequesttable.Find(drid);
            d.status = "Accept";
            db.SaveChanges();
            project pp = db.projecttable.Find(poid);
            user u = db.usertable.Find(ooo);
            db.Entry(pp).Reference("leader").CurrentValue = u;
            db.SaveChanges();

            
            return RedirectToAction("TeamleaderRequests");
        }
        public ActionResult RefuseRequest(int poid,int drid)
        {
            int ooo = (int)Session["id"];
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == ooo select new viewmodel { u = e };
            ViewData["mdinfo"] = x.ToList();
            drrequest d = db.drrequesttable.Find(drid);
            d.status = "Refuse";
            db.SaveChanges();
           


            return RedirectToAction("TeamleaderRequests");
        }
        public ActionResult TeamLeaderLEave(int poid)
        {
            int ooo = (int)Session["id"];
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == ooo select new viewmodel { u = e };
            ViewData["mdinfo"] = x.ToList();
            project pp = db.projecttable.Find(poid);
            db.Entry(pp).Reference("Manager").Load();
            user Manager = db.usertable.Find(pp.Manager.userid);
            user leader = db.usertable.Find(ooo);
            userrequest ur = new userrequest();
            ur.projects = pp;
            ur.sender = leader;
            ur.reciver = Manager;
            ur.request = "can i leave this project for some personal reasons";
            ur.satuts = "non seen";
            db.urequesttable.Add(ur);
            db.SaveChanges();
            
            return RedirectToAction("TeamleaderCurrent");
        }
        public ActionResult TeamLeaderQualifications()
        {
            int ooo = (int)Session["id"];
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == ooo select new viewmodel { u = e };
            ViewData["mdinfo"] = x.ToList();
            var finshiedproject = from e in db.projecttable where e.leader.userid == ooo & e.status == "finished" select e;
            var wokringprojects = from e in db.projecttable where e.leader.userid == ooo & e.status != "finished" select e;
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

            ViewData["allprojects"] = count2;
            ViewData["finishprojects"] = count;
            ViewData["workingprojects"] = count1;
            ViewData["feedbacks"] = count3;
            return View();
        }
        public ActionResult MDLeavingRequests()
        {
            int ooo = (int)Session["id"];
            var x = from e in db.usertable join p in db.usertable on e.userid equals p.userid where e.userid == ooo select new viewmodel { u = e };
            ViewData["mdinfo"] = x.ToList();
            var request = from e in db.urequesttable join p in db.projecttable on e.projects.projectid equals p.projectid join u in db.usertable on e.sender.userid equals u.userid where e.reciver.userid == ooo & e.satuts == "non seen" select new viewmodel { u = u, p = p, r = e };
            return View(request);
        }
        public ActionResult AcceptLeaving(int reid)
        {
            userrequest r = db.urequesttable.Find(reid);
            r.satuts = "Accept";
            db.Entry(r).Reference("sender").Load();
            db.Entry(r).Reference("projects").Load();
            user u = db.usertable.Find(r.sender.userid);
            project p = db.projecttable.Find(r.projects.projectid);
            db.SaveChanges();
            if (u.status.Equals("Marketing team leader"))
            {
                db.Entry(p).Reference("leader").CurrentValue = null;
                db.SaveChanges();
            }
            else
            {
                var o = from e in db.worksontable where e.trainee.userid == u.userid & e.workignproject.projectid == p.projectid select e;
                db.worksontable.Remove((workson)o);
                db.SaveChanges();
            }


            return RedirectToAction("MDLeavingRequests");

        }
        public ActionResult RefuseLeaving(int reid)
        {
            userrequest r = db.urequesttable.Find(reid);
            r.satuts = "Refuse";
            db.SaveChanges();
            return RedirectToAction("MDRequests");




        }

    }
}