using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1;

namespace WebApplication1.Controllers
{
    public class tbTasksController : Controller
    {
        private TodoDBEntities db = new TodoDBEntities();

        // GET: tbTasks
        public ActionResult Index()
        {
            
            return View(db.tbTasks.ToList());
        }
        //incompletetasks
        public ActionResult Incomplete()
        {
            string temp = "Incomplete";
            using (var context=new TodoDBEntities())
            {
                return View(context.tbTasks.Where(e=>e.Status == temp).ToList());
                
            }
           // return View(db.tbTasks.ToList());
        }

        public ActionResult Completed()
        {
            string temp = "Completed";
            using (var context = new TodoDBEntities())
            {
                return View(context.tbTasks.Where(e => e.Status == temp).ToList());

            }
            // return View(db.tbTasks.ToList());
        }
       
        public ActionResult MarkCompleted(int id)
        {
            
            using (var context = new TodoDBEntities())
            {
                
                var taskdata = context.tbTasks.Where(e => e.ID == id).FirstOrDefault();
                if (taskdata != null)
                {
                    taskdata.Status = "Completed";
                    context.Entry(taskdata).State = EntityState.Modified;
                    context.SaveChanges();
                    ModelState.AddModelError("", "Task Completed!!");
                  
                }
                return RedirectToAction("Incomplete");

            }

        }
        public ActionResult MarkIncompleted(int id)
        {

            using (var context = new TodoDBEntities())
            {

                var taskdata = context.tbTasks.Where(e => e.ID == id).FirstOrDefault();
                if (taskdata != null)
                {
                    taskdata.Status = "Incomplete";
                    context.Entry(taskdata).State = EntityState.Modified;
                    context.SaveChanges();
                    //ModelState.AddModelError("", "Task Completed!!");

                }
                return RedirectToAction("Completed");

            }

        }
        



        // GET: tbTasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTask tbTask = db.tbTasks.Find(id);
            if (tbTask == null)
            {
                return HttpNotFound();
            }
            return View(tbTask);
        }

        // GET: tbTasks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tbTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Task,Status")] tbTask tbTask)
        {
            if (ModelState.IsValid)
            {
                if (tbTask.Task == null)
                {
                    ModelState.AddModelError("", "Task Can't be null!!");
                }
                else
                {
                    tbTask.Status = "Incomplete";
                    db.tbTasks.Add(tbTask);
                    db.SaveChanges();
                    tbTask.Task = "";
                    //tbTask objtbTask = new tbTask();
                    return View("Create");
                    //return RedirectToAction("Index");
                }
               
            }
            
           return View();
        }

        // GET: tbTasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTask tbTask = db.tbTasks.Find(id);
            if (tbTask == null)
            {
                return HttpNotFound();
            }
            return View(tbTask);
        }

        // POST: tbTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Task,Status")] tbTask tbTask)
        {
            if (ModelState.IsValid)
            {
                if (tbTask.Status=="Completed")
                {
                    tbTask.Status = "Completed";
                    db.Entry(tbTask).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                else
                {
                    tbTask.Status = "Inomplete";
                    db.Entry(tbTask).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }
            return View(tbTask);
        }

        // GET: tbTasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTask tbTask = db.tbTasks.Find(id);
            if (tbTask == null)
            {
                return HttpNotFound();
            }
            return View(tbTask);
        }

        // POST: tbTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbTask tbTask = db.tbTasks.Find(id);
            db.tbTasks.Remove(tbTask);
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
