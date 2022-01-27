using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using school_adar.Models;

namespace school_adar.Controllers
{
    public class LesseesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Lessees
        public ActionResult Index()
        {
            var data = db.Lessee.ToList();
            return View(data);
        }

        // GET: Lessees/DisplayImage
        [HttpGet]
        public string DisplayImage(string id="2")
        {
            var ide = int.Parse(id);
            var data = db.Lessee.Find(ide);
            byte[] image = data.Image;
            string contentType = "image/jpeg";
            ViewBag.imageSrc = string.Format("data:{0};base64,{1}",contentType, Convert.ToBase64String(image));
            //MemoryStream ms = new MemoryStream(image);
            //Image img = Image.FromStream(ms);
            return ViewBag.imageSrc;
            //ViewBag.imageSrc,"text/plain",Encoding.UTF8
        }

        // GET: Lessees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lessee lessee = db.Lessee.Find(id);
            if (lessee == null)
            {
                return HttpNotFound();
            }
            return View(lessee);
        }

        // GET: Lessees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lessees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,PhoneNo,Email,Adress,Image")] Lessee lessee, HttpPostedFileBase ImageFile)
        {
            var files = Request.Files["ImageFile"];
            
            using (var ms = new MemoryStream())
            {
                files.InputStream.CopyTo(ms);
                lessee.Image = ms.ToArray();
            }

            if (ModelState.IsValid)
            {
                db.Lessee.Add(lessee);
                db.SaveChanges();
                return RedirectToAction("Index", "Housings");
            }

            return View(lessee);
        }

        // GET: Lessees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lessee lessee = db.Lessee.Find(id);
            if (lessee == null)
            {
                return HttpNotFound();
            }
            return View(lessee);
        }

        // POST: Lessees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,PhoneNo,Email,Adress,Image")] Lessee lessee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lessee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lessee);
        }

        // GET: Lessees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lessee lessee = db.Lessee.Find(id);
            if (lessee == null)
            {
                return HttpNotFound();
            }
            return View(lessee);
        }

        // POST: Lessees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lessee lessee = db.Lessee.Find(id);
            db.Lessee.Remove(lessee);
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
