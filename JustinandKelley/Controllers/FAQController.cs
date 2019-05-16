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
    public class FAQController : BaseController
    {
        private JustinandKelleyEntities db = new JustinandKelleyEntities();

        //
        // GET: /FAQ/

        public ViewResult Index()
        {
            return View(db.FAQs.ToList());
        }

        //
        // GET: /FAQ/Details/5

        public ViewResult Details(int id)
        {
            FAQ faq = db.FAQs.Single(f => f.ID == id);
            return View(faq);
        }

        //
        // GET: /FAQ/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /FAQ/Create

        [HttpPost]
        public ActionResult Create(FAQ faq)
        {
            if (ModelState.IsValid)
            {
                db.FAQs.AddObject(faq);
                db.SaveChanges();
                try
                {
                    SendQAEmail(faq);
                }
                catch
                {
                    //do nothing
                }
                return RedirectToAction("Index");  
            }

            return View(faq);
        }

        private void SendQAEmail(FAQ faq)
        {
            String subject = "New Wedding Question";
            string body = "Question: " + faq.Question;
            RSVPController.SendEmail(body, subject);
        }
        
        //
        // GET: /FAQ/Edit/5
 
        public ActionResult Edit(int id)
        {
            FAQ faq = db.FAQs.Single(f => f.ID == id);
            return View(faq);
        }

        //
        // POST: /FAQ/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FAQ faq)
        {
            if (ModelState.IsValid)
            {
                db.FAQs.Attach(faq);
                db.ObjectStateManager.ChangeObjectState(faq, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(faq);
        }

        //
        // GET: /FAQ/Delete/5
 
        public ActionResult Delete(int id)
        {
            FAQ faq = db.FAQs.Single(f => f.ID == id);
            return View(faq);
        }

        //
        // POST: /FAQ/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            FAQ faq = db.FAQs.Single(f => f.ID == id);
            db.FAQs.DeleteObject(faq);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}