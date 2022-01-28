using System;
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
    public class RequestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Requests
        public ActionResult Index(int? id)
        {
            List<Request> request = db.Request.Where(temp => temp.Housing.LessorID == id).ToList();
            return View(request);
        }

        // GET: Requests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Request.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // GET: Requests/Create
        public ActionResult Create()
        {
            ViewBag.HousingID = new SelectList(db.Housings, "ID", "Location");
            ViewBag.LesseeID = new SelectList(db.Lessee, "ID", "FirstName");
            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LesseeID,HousingID")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Request.Add(request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HousingID = new SelectList(db.Housings, "ID", "Location", request.HousingID);
            ViewBag.LesseeID = new SelectList(db.Lessee, "ID", "FirstName", request.LesseeID);
            return View(request);
        }

        // GET: Requests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Request.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            ViewBag.HousingID = new SelectList(db.Housings, "ID", "Location", request.HousingID);
            ViewBag.LesseeID = new SelectList(db.Lessee, "ID", "FirstName", request.LesseeID);
            return View(request);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,LesseeID,HousingID")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HousingID = new SelectList(db.Housings, "ID", "Location", request.HousingID);
            ViewBag.LesseeID = new SelectList(db.Lessee, "ID", "FirstName", request.LesseeID);
            return View(request);
        }

        // GET: Requests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Request.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Request request = db.Request.Find(id);
            db.Request.Remove(request);
            db.SaveChanges();
            return RedirectToAction("Index", "Requests");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult SendRequest(int houseID)
        {
            Request request = new Request();
            Lessee lessee = db.Lessee.Where(tmp => tmp.Email == User.Identity.Name).FirstOrDefault();
            request.LesseeID = lessee.ID;
            request.HousingID = houseID;
            
            if (ModelState.IsValid)
            {
                db.Request.Add(request);
                db.SaveChanges();
                return RedirectToAction("LesseeDetails", "Housings", new { id = houseID });
            }
            return View(request);
        }
       
    }
}
