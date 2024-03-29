﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using school_adar.Models;

namespace school_adar.Controllers
{
    public class LessorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Lessors
        public ActionResult Index()
        {
            return View(db.Lessor.ToList());
        }

        // GET: Lessors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lessor lessor = db.Lessor.Find(id);
            if (lessor == null)
            {
                return HttpNotFound();
            }
            return View(lessor);
        }

        // GET: Lessors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lessors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,PhoneNo,Email,Address")] Lessor lessor)
        {
            if (ModelState.IsValid)
            {
                db.Lessor.Add(lessor);
                db.SaveChanges();
                return RedirectToAction("LessorHouses", "Housings", new { id = lessor.ID}); 
            }

            return View(lessor);
        }

        // GET: Lessors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lessor lessor = db.Lessor.Find(id);
            if (lessor == null)
            {
                return HttpNotFound();
            }
            return View(lessor);
        }

        // POST: Lessors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,PhoneNo,Email,Address")] Lessor lessor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lessor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lessor);
        }

        // GET: Lessors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lessor lessor = db.Lessor.Find(id);
            if (lessor == null)
            {
                return HttpNotFound();
            }
            return View(lessor);
        }

        // POST: Lessors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lessor lessor = db.Lessor.Find(id);
            db.Lessor.Remove(lessor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult getLessorID()
        {
            Lessor lessor = db.Lessor.Where(tmp => tmp.Email == User.Identity.Name).FirstOrDefault();
            if (lessor != null)
            {
                return RedirectToAction("Index", "Requests", new { id = lessor.ID });
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
