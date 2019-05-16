using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JustinandKelley.Models;

namespace JustinandKelley.Controllers
{
    public class RSVPController : BaseController
    {
        private JustinandKelleyEntities _db = new JustinandKelleyEntities();

        //
        // GET: /RSVP/

        public ViewResult Index()
        {
            return View(_db.RSVPs.ToList());
        }

        //
        // GET: /RSVP/Details/5

        public ViewResult Details(int id)
        {
            RSVP rsvp = _db.RSVPs.Single(r => r.ID == id);
            return View(rsvp);
        }

        //
        // GET: /RSVP/Create

        public ActionResult Create()
        {
            return View();
        }
        public ActionResult ThankYou(String Name)
        {
            ViewBag.RSVPFirstName = Name;
            return View();
        }

        //
        // POST: /RSVP/Create

        [HttpPost]
        public ActionResult Create(RSVP rsvp)
        {
            if (ModelState.IsValid)
            {
                _db.RSVPs.AddObject(rsvp);
                _db.SaveChanges();
                try
                {
                    SendRSVPEmail(rsvp);
                }
                catch 
                {
                    //Do Nothing
                }

                return RedirectToAction("ThankYou", "RSVP", new { Name = rsvp.FirstName });
            }

            return View(rsvp);
        }

        private void SendRSVPEmail(RSVP rsvp)
        {

            string emailSubject = "Wedding RSVP";
            String body = "";
            body = "Name: " + String.Format("{0} {1}", rsvp.FirstName, rsvp.LastName) +
                Environment.NewLine + "Email: " + rsvp.Email +
                Environment.NewLine + "Phone: " + rsvp.Phone +
                Environment.NewLine + "Number of People: " + rsvp.NumberOfPeople.ToString() +
                Environment.NewLine + "Comments: " + rsvp.Comments;

            SendEmail(body, emailSubject);
        }

        public static void SendEmail(String body, String subject)
        {
            String serverName = "relay-hosting.secureserver.net";
            String emailFrom = "mrwizard@justinandkelley.net";
            string emailTo = "justin.r.smits@gmail.com,kelleyadarnell@gmail.com";
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage(emailFrom, emailTo, subject, body);
            System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient(serverName, 25);
            smtpClient.EnableSsl = false;
            smtpClient.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
            smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            smtpClient.Send(msg);
        }

        //
        // GET: /RSVP/Edit/5

        public ActionResult Edit(int id)
        {
            RSVP rsvp = _db.RSVPs.Single(r => r.ID == id);
            return View(rsvp);
        }

        //
        // POST: /RSVP/Edit/5

        [HttpPost]
        public ActionResult Edit(RSVP rsvp)
        {
            if (ModelState.IsValid)
            {
                _db.RSVPs.Attach(rsvp);
                _db.ObjectStateManager.ChangeObjectState(rsvp, EntityState.Modified);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rsvp);
        }

        //
        // GET: /RSVP/Delete/5

        public ActionResult Delete(int id)
        {
            RSVP rsvp = _db.RSVPs.Single(r => r.ID == id);
            return View(rsvp);
        }

        //
        // POST: /RSVP/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            RSVP rsvp = _db.RSVPs.Single(r => r.ID == id);
            _db.RSVPs.DeleteObject(rsvp);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}