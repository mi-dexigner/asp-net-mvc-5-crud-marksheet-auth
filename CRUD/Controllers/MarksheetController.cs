using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net; // using for 404 function
using System.Web.Mvc;

namespace CRUD.Controllers
{
    public class MarksheetController : Controller
    {
        authLoginEntities db = new authLoginEntities();
        // GET: Marksheet
        public ActionResult Index()
        {
            var data = db.marksheets.ToList();

            return View(data);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection fc)
        {
            marksheet udata = new marksheet();
            udata.FirstName = Request.Form["FirstName"];
            udata.LastName = Request.Form["LastName"];
            udata.EnglishMark = int.Parse(Request.Form["EnglishMark"]);
            udata.MathMark = int.Parse(Request.Form["MathMark"]);
            udata.UrduMark = int.Parse(Request.Form["UrduMark"]);
            udata.TotalMark = 300;
            udata.ObtainMark = udata.EnglishMark + udata.UrduMark + udata.MathMark;
            float percent = udata.ObtainMark * 100 / udata.TotalMark;
            udata.Percentages = percent;

            switch (percent)
            {
                case 80:
                   udata.Grade =  "A+";
                    break;
                case 70:
                    udata.Grade = "A";
                    break;
                case 60:
                    udata.Grade = "B";
                    break;
                case 50:
                    udata.Grade = "C";
                    break;
                case 40:
                    udata.Grade = "D";
                    break;
                     default:
                    udata.Grade = "F";
                    break;
            }

            db.marksheets.Add(udata);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            marksheet data = db.marksheets.Find(id);

            if (data == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            else
            {
                return View(data);
            }

        }

        [HttpPost, ActionName("Delete")]

        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            marksheet data = db.marksheets.Find(id);

            db.marksheets.Remove(data);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            marksheet data = db.marksheets.Find(id);
            if (data == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            else
            {
                return View(data);
            }

        }

         [HttpPost]
        public ActionResult Edit(FormCollection fc)
        {
            marksheet udata = new marksheet();
            udata.id = int.Parse(Request.Form["id"]);
            udata.FirstName = Request.Form["FirstName"];
            udata.LastName = Request.Form["LastName"];
            udata.EnglishMark = int.Parse(Request.Form["EnglishMark"]);
            udata.MathMark = int.Parse(Request.Form["MathMark"]);
            udata.UrduMark = int.Parse(Request.Form["UrduMark"]);
            udata.TotalMark = 300;
            udata.ObtainMark = udata.EnglishMark + udata.UrduMark + udata.MathMark;

            float percent = udata.ObtainMark * 100 / udata.TotalMark;
            udata.Percentages = percent;

            switch (percent)
            {
                case 80:
                    udata.Grade = "A+";
                    break;
                case 70:
                    udata.Grade = "A";
                    break;
                case 60:
                    udata.Grade = "B";
                    break;
                case 50:
                    udata.Grade = "C";
                    break;
                case 40:
                    udata.Grade = "D";
                    break;
                default:
                    udata.Grade = "F";
                    break;
            }
            db.Entry(udata).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            marksheet data = db.marksheets.Find(id);
            if (data == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            else
            {
                return View(data);
            }
        }

    }
}