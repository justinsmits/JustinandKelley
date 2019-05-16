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
    public class AuditController : Controller
    {
        private JustinandKelleyEntities db = new JustinandKelleyEntities();

        //
        // GET: /Audit/

        public ViewResult Index()
        {
            return View(db.AuditHistories.ToList());
        }

        //
        // GET: /Audit/Details/5

        public ViewResult Details(int id)
        {
            AuditHistory audithistory = db.AuditHistories.Single(a => a.ID == id);
            return View(audithistory);
        }

        //
        // GET: /Audit/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Audit/Create

        [HttpPost]
        public ActionResult Create(AuditHistory audithistory)
        {
            if (ModelState.IsValid)
            {
                db.AuditHistories.AddObject(audithistory);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(audithistory);
        }
        
        //
        // GET: /Audit/Edit/5
 
        public ActionResult Edit(int id)
        {
            AuditHistory audithistory = db.AuditHistories.Single(a => a.ID == id);
            return View(audithistory);
        }

        //
        // POST: /Audit/Edit/5

        [HttpPost]
        public ActionResult Edit(AuditHistory audithistory)
        {
            if (ModelState.IsValid)
            {
                db.AuditHistories.Attach(audithistory);
                db.ObjectStateManager.ChangeObjectState(audithistory, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(audithistory);
        }

        //
        // GET: /Audit/Delete/5
 
        public ActionResult Delete(int id)
        {
            AuditHistory audithistory = db.AuditHistories.Single(a => a.ID == id);
            return View(audithistory);
        }

        //
        // POST: /Audit/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            AuditHistory audithistory = db.AuditHistories.Single(a => a.ID == id);
            db.AuditHistories.DeleteObject(audithistory);
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