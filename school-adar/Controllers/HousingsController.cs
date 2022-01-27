using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using school_adar.Models;

namespace school_adar.Controllers
{
    public class HousingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Housings
        public ActionResult Index()
        {
            var housings = db.Housings.Include(h => h.Lessor);
            return View(housings.ToList());
        }

        // GET: Housings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Housing housing = db.Housings.Find(id);
            if (housing == null)
            {
                return HttpNotFound();
            }
            return View(housing);
        }

        // GET: Housings/Create
        public ActionResult Create()
        {
            //ViewBag.LessorID = new SelectList(db.Lessor, "ID", "FirstName");
            return View();
        }

        // POST: Housings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LessorID,Location,Size,Price,Image,Features,Condition")] Housing housing, HttpPostedFileBase ImageFile)
        {
            var files = Request.Files["ImageFile"];

            using (var ms = new MemoryStream())
            {
                files.InputStream.CopyTo(ms);
                housing.Image = ms.ToArray();
            }
    
            Lessor lessor = db.Lessor.Where(tmp => tmp.Email == User.Identity.Name).FirstOrDefault();
            housing.LessorID = lessor.ID;
           
            if (ModelState.IsValid)
            {
                db.Housings.Add(housing);
                db.SaveChanges();
                return RedirectToAction("LessorHouses", "Housings", new { id = lessor.ID });
            }
            
            //ViewBag.LessorID = new SelectList(db.Lessor, "ID", "FirstName", housing.LessorID);
            return View(housing);
        }

        // GET: Housings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Housing housing = db.Housings.Find(id);
            if (housing == null)
            {
                return HttpNotFound();
            }
            ViewBag.LessorID = new SelectList(db.Lessor, "ID", "FirstName", housing.LessorID);
            return View(housing);
        }

        // POST: Housings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,LessorID,Location,Size,Price,Image,Features,Condition")] Housing housing, HttpPostedFileBase ImageFile)
        {
            var files = Request.Files["ImageFile"];

            using (var ms = new MemoryStream())
            {
                files.InputStream.CopyTo(ms);
                housing.Image = ms.ToArray();
            }
            
            Lessor lessor = db.Lessor.Where(tmp => tmp.Email == User.Identity.Name).FirstOrDefault();
            housing.LessorID = lessor.ID;

            if (ModelState.IsValid)
            {
                db.Entry(housing).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("LessorHouses", "Housings", new { id = housing.LessorID });
            }
            ViewBag.LessorID = new SelectList(db.Lessor, "ID", "FirstName", housing.LessorID);
            return View(housing);
        }

        // GET: Housings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Housing housing = db.Housings.Find(id);
            if (housing == null)
            {
                return HttpNotFound();
            }
            return View(housing);
        }

        // POST: Housings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Housing housing = db.Housings.Find(id);
            db.Housings.Remove(housing);
            db.SaveChanges();
            return RedirectToAction("LessorHouses", "Housings", new { id = housing.LessorID });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult LessorHouses(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Housing> housing = db.Housings.Where(temp=>temp.LessorID == id).ToList();
            if (housing == null)
            {
                return HttpNotFound();
            }
            return View(housing);
        }
    }
}
