using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        //instance from database
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index()
        {
            var list = db.Categories.ToList(); //convert DB categories into list
            return View(list);
        }

        public ActionResult Details(int JobId)
        {
            var job = db.Jobs.Find(JobId);
            if (job == null)
            {
                return HttpNotFound();
            }
            Session["JobId"] = JobId;
            return View(job);
        }

        // this will get the form message from the Applier
        [Authorize]
        public ActionResult Apply()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Apply(String Message)
        {
            var UserId = User.Identity.GetUserId();         // get the user Id but he must be logged In
            var JobId = (int)Session["JobId"]; // in details function

            var check = db.ApplyForJobs.Where(a => a.JobId == JobId && a.UserId == UserId).ToList();
            if (check.Count < 1)
            {
                var job = new ApplyForJob();
                job.UserId = UserId;
                job.JobId = JobId;
                job.Message = Message;
                job.ApplyDate = DateTime.Now;


                db.ApplyForJobs.Add(job);
                db.SaveChanges();
                ViewBag.Result = "تمت الاضافة بنجاح !";
            }
            else
            {
                // error message from action to view.
                ViewBag.Result = "المعذرة، لقد سبق وتقدمت الي نفس الوظيفة!";
            }

            return View();
        }

        //public ActionResult GetJobByUser()
        //{
        //var UserId = User.Identity.GetUserId();
        //var Jobs = db.ApplyForJobs.Where(a => a.UserId == UserId);
        //  return View(Jobs.ToList());
        //}

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}