using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using siteVote.Models;

namespace siteVote.Controllers
{
    public class VoteController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Vote/
        public ActionResult Index()
        {
            return View(db.VoteSheet.ToList());
        }

        // GET: /Vote/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VoteSheet votesheet = db.VoteSheet.Find(id);
            if (votesheet == null)
            {
                return HttpNotFound();
            }
            return View(votesheet);
        }

        // GET: /Vote/Create
        public ActionResult Create()
        {
            List<VoteViewModels> model = new List<VoteViewModels>();
            
            
            foreach (var item in db.Site.ToList())
            {
                model.Add(new VoteViewModels { siteName = item.name });
            }

            return View(model.ToList());
        }

        // POST: /Vote/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(List<VoteViewModels> model)
        {
            if (ModelState.IsValid)
            {
                
                VoteSheet voteSheet = new VoteSheet();
                voteSheet.Votes = new List<Vote>();
                foreach (var item in model)
                {
                    voteSheet.Votes.Add(new Vote() { Site=db.Site.First(a=>a.name==item.siteName),Score=item.score});
                    
                }
                db.VoteSheet.Add(voteSheet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: /Vote/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VoteSheet votesheet = db.VoteSheet.Find(id);
            if (votesheet == null)
            {
                return HttpNotFound();
            }
            return View(votesheet);
        }

        // POST: /Vote/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id")] VoteSheet votesheet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(votesheet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(votesheet);
        }

        // GET: /Vote/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VoteSheet votesheet = db.VoteSheet.Find(id);
            if (votesheet == null)
            {
                return HttpNotFound();
            }
            return View(votesheet);
        }

        // POST: /Vote/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VoteSheet votesheet = db.VoteSheet.Find(id);
            db.VoteSheet.Remove(votesheet);
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
