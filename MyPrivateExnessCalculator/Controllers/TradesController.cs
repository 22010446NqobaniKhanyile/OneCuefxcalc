using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyPrivateExnessCalculator.Models;

namespace MyPrivateExnessCalculator.Controllers
{
    public class TradesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Trades
        public ActionResult Index()
        {
            return View(db.Trades.ToList().Where(x => x.Results == "Pending"));
        }
        public PartialViewResult WinResults()

        {
            WinCont();
            return PartialView(db.Trades.ToList().Where(x => x.Results == "Win"));
        }
        public PartialViewResult LooseResults()


        {
            LossCont();
            return PartialView(db.Trades.ToList().Where(x => x.Results == "Loose"));
        }

        public void WinCont()
		{
            ViewBag.Count = db.Trades.ToList().Where(x => x.Results == "Win").Count();

        }
        public void LossCont()

        {
            ViewBag.Count = db.Trades.ToList().Where(x => x.Results == "Loose").Count();
        }
        public ActionResult Result()

        {
            return View();
        }

        // GET: Trades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trade trade = db.Trades.Find(id);
            if (trade == null)
            {
                return HttpNotFound();
            }
            return View(trade);
        }

        // GET: Trades/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Trades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TradeId,Date,Pair,Entry,StopLoss,StopLossPoints,TP,PipValue,OnePipette,Margin,MinLot,FinalLot,StopLossValue,Results,HitStop,Gain,Loss")] Trade trade)
        {
            if (ModelState.IsValid)
            {
               
                //if buy trade
                if (trade.Entry>trade.StopLoss)
				{
                    trade.StopLossPoints = trade.Entry - trade.StopLoss;

                }
                else if(trade.Entry < trade.StopLoss) //sell trade
				{
                    trade.StopLossPoints = trade.StopLoss - trade.Entry;
                }
                trade.OnePipette = trade.PipValue / 10;
                trade.MinLot = 0.01M;
                trade.FinalLot = trade.MinLot;
                trade.StopLossValue = trade.OnePipette * trade.StopLossPoints;
                trade.Results = "Pending";
                db.Trades.Add(trade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trade);
        }

        // GET: Trades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trade trade = db.Trades.Find(id);
            if (trade == null)
            {
                return HttpNotFound();
            }
            return View(trade);
        }

        // POST: Trades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TradeId,Date,Pair,Entry,StopLoss,StopLossPoints,TP,PipValue,OnePipette,Margin,MinLot,FinalLot,StopLossValue,Results,HitStop,Gain,Loss")] Trade trade)
        {
            if (ModelState.IsValid)
            {
                decimal temp;
                temp = trade.FinalLot * 100;

                trade.StopLossValue = trade.StopLossValue * temp;

                trade.Margin = trade.Margin * temp;
                
                db.Entry(trade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trade);
        }

        public ActionResult ResultsUpdate(int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trade trade = db.Trades.Find(id);
            if (trade == null)
            {
                return HttpNotFound();
            }
            return View(trade);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResultsUpdate([Bind(Include = "TradeId,Date,Pair,Entry,StopLoss,StopLossPoints,TP,PipValue,OnePipette,Margin,MinLot,FinalLot,StopLossValue,Results,HitStop,Gain,Loss")] Trade trade)

        {
            if (ModelState.IsValid)
            {
                db.Entry(trade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trade);
        }
        public ActionResult WinUpdate(int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trade trade = db.Trades.Find(id);
            if (trade == null)
            {
                return HttpNotFound();
            }
            return View(trade);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WinUpdate([Bind(Include = "TradeId,Date,Pair,Entry,StopLoss,StopLossPoints,TP,PipValue,OnePipette,Margin,MinLot,FinalLot,StopLossValue,Results,HitStop,Gain,Loss")] Trade trade)


        {
            if (ModelState.IsValid)
            {
                db.Entry(trade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Result");
            }
            return View(trade);
        }


        public ActionResult LossUpdate(int? id)


        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trade trade = db.Trades.Find(id);
            if (trade == null)
            {
                return HttpNotFound();
            }
            return View(trade);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LossUpdate([Bind(Include = "TradeId,Date,Pair,Entry,StopLoss,StopLossPoints,TP,PipValue,OnePipette,Margin,MinLot,FinalLot,StopLossValue,Results,HitStop,Gain,Loss")] Trade trade)



        {
            if (ModelState.IsValid)
            {
                db.Entry(trade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Result");
            }
            return View(trade);
        }
        // GET: Trades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trade trade = db.Trades.Find(id);
            if (trade == null)
            {
                return HttpNotFound();
            }
            return View(trade);
        }

        // POST: Trades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trade trade = db.Trades.Find(id);
            db.Trades.Remove(trade);
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
