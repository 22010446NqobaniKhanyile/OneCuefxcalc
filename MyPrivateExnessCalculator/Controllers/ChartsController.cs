using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyPrivateExnessCalculator.Models;

namespace MyPrivateExnessCalculator.Controllers
{
    public class ChartsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Charts
        public PartialViewResult Index()
        {
            return PartialView(db.Charts.ToList());
        }

        // GET: Charts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chart chart = db.Charts.Find(id);
            if (chart == null)
            {
                return HttpNotFound();
            }
            return View(chart);
        }

        [HttpGet]

        // GET: Charts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Charts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Chart chart)
        {

            string fileName = Path.GetFileNameWithoutExtension(chart.ImageFile.FileName);
            string extension = Path.GetExtension(chart.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            chart.ImagePath = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            chart.ImageFile.SaveAs(fileName);
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Charts.Add(chart);
                db.SaveChanges();
            }
            ModelState.Clear();
            return RedirectToAction("Result", "Trades");
        }

        // GET: Charts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chart chart = db.Charts.Find(id);
            if (chart == null)
            {
                return HttpNotFound();
            }
            return View(chart);
        }

        // POST: Charts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChartID,Pair,Date,ImagePath")] Chart chart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chart);
        }

        // GET: Charts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chart chart = db.Charts.Find(id);
            if (chart == null)
            {
                return HttpNotFound();
            }
            return View(chart);
        }

        // POST: Charts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Chart chart = db.Charts.Find(id);
            db.Charts.Remove(chart);
            db.SaveChanges();
            return RedirectToAction("Result", "Trades");
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
