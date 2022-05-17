using Microsoft.AspNet.Identity;
using Jop_Offers_Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using something.Models;
using System.Data.Entity;
using System.Net.Mail;
using System.Net;

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

        [Authorize]
        public ActionResult GetJobByUser()
        {
            var UserId = User.Identity.GetUserId();
            var Jobs = db.ApplyForJobs.Where(a => a.UserId == UserId);
            return View(Jobs.ToList());
        }

        public ActionResult DetailsOfJob(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        [Authorize]
        public ActionResult GetJobsByPublisher()
        {
            var UserID = User.Identity.GetUserId();

            var Jobs = from app in db.ApplyForJobs
                       join job in db.Jobs
                       on app.JobId equals job.Id
                       where job.User.Id == UserID
                       select app;

            var grouped = from j in Jobs
                          group j by j.job.JobTitle
                          into gr
                          select new JobsViewModel
                          {
                              JobTitle = gr.Key,
                              Items = gr
                          };

            return View(grouped.ToList());
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
                return HttpNotFound();
            return View(job);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        public ActionResult Edit(ApplyForJob job)
        {
            if (ModelState.IsValid)
            {
                job.ApplyDate = DateTime.Now;
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetJobByUser");
            }
            return View(job);
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
                return HttpNotFound();
            return View(job);
        }

        // POST: Roles/Delete/5
        [HttpPost]
        public ActionResult Delete(ApplyForJob job)
        {
                // TODO: Add delete logic here
                var myJob = db.ApplyForJobs.Find(job.Id);
                db.ApplyForJobs.Remove(myJob);
                db.SaveChanges();
                return RedirectToAction("GetJobByUser");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactModel contact)
        {
            var mail = new MailMessage();
            var loginInfo = new NetworkCredential("level.3.cs.is@gmail.com", "ujpjchkomcdrxvpz", "smtp.gmail.com");
            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress("level.3.cs.is@gmail.com"));
            mail.Subject = contact.Subject;
            mail.IsBodyHtml = true;  //عشان أخلي الايميل على شكل Html
            string body = "اسم المرسل: " + contact.Name + "<br>" +
                "بريد المرسل: " + contact.Email + "<br>" +
                "عنوان الرسالة: " + contact.Subject + "<br>" +
                "نص الرسالة: " + contact.Message + "<br>";
            mail.Body = body;

            //To send email in .Net
            var smtpClient = new SmtpClient("smtp.gmail.com", 587); //Port of gmail
            smtpClient.EnableSsl = true; //الوضع الآمن في عملية تحويل البيانات من الBrowser=>webserver  
            smtpClient.Credentials = loginInfo;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Send(mail);
            return RedirectToAction("Index");
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var result = db.Jobs.Where(a => a.JobTitle.Contains(searchName)
            || a.JobContent.Contains(searchName)
            || a.Category.CategoryName.Contains(searchName)
            || a.Category.CategoryDescription.Contains(searchName)).ToList();
            
            return View(result);
        }
    }
}

