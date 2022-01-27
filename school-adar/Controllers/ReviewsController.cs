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
    public class ReviewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reviews
        public ActionResult Index()
        {
            var review = db.Review.Include(r => r.Housing).Include(r => r.Lessee);
            return View(review.ToList());
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Review.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Reviews/Create
        public ActionResult Create()
        {
            //ViewBag.HousingID = new SelectList(db.Housings, "ID", "Location");
            //ViewBag.LesseeID = new SelectList(db.Lessee, "ID", "FirstName");
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LesseeID,HousingID,Rating,Comment")] Review review)
        {

            if (ModelState.IsValid)
            {
                db.Review.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.HousingID = new SelectList(db.Housings, "ID", "Location", review.HousingID);
            //ViewBag.LesseeID = new SelectList(db.Lessee, "ID", "FirstName", review.LesseeID);
            return View(review);
        }

        public ActionResult GiveRating(int houseID, Double rating)
        {
            Review review = new Review();
            Lessee lessee = db.Lessee.Where(tmp => tmp.Email == User.Identity.Name).FirstOrDefault();
            Housing housing = db.Housings.Where(tmp => tmp.ID == houseID).FirstOrDefault();
            review.LesseeID = lessee.ID;
            review.HousingID = houseID;
            review.Rating = rating;

            if (ModelState.IsValid)
            {
                db.Review.Add(review);
                db.SaveChanges();
                return RedirectToAction("LesseeDetails", "Housings", new { id = housing.ID });
            }
            return View(review);
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Review.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.HousingID = new SelectList(db.Housings, "ID", "Location", review.HousingID);
            ViewBag.LesseeID = new SelectList(db.Lessee, "ID", "FirstName", review.LesseeID);
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,LesseeID,HousingID,Rating,Comment")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HousingID = new SelectList(db.Housings, "ID", "Location", review.HousingID);
            ViewBag.LesseeID = new SelectList(db.Lessee, "ID", "FirstName", review.LesseeID);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Review.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Review.Find(id);
            db.Review.Remove(review);
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
    }
}
