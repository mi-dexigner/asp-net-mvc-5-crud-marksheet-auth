using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net; // using for 404 function
using System.Web.Mvc;
using System.IO;

namespace CRUD.Controllers
{
    public class HomeController : Controller
    {
        authLoginEntities db = new authLoginEntities();
        public ActionResult Index()
        {
            var data = db.auths.ToList();
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection fc,HttpPostedFileBase profilepic)
        {
            auth udata = new auth();
            string _FileName = Path.GetFileNameWithoutExtension(profilepic.FileName);
            string _FileExe = Path.GetExtension(profilepic.FileName);
            Random rnd = new Random();
            var imgSave = _FileName + rnd.Next() + _FileExe;
            string _path = Path.Combine(Server.MapPath("~/uploads"), imgSave);
            profilepic.SaveAs(_path);

            udata.profilepic = imgSave;
            udata.fullname = Request.Form["fullname"];
            udata.email = Request.Form["email"];
            udata.password = Request.Form["password"];
            udata.phone = Request.Form["phone"];
            db.auths.Add(udata);
            db.SaveChanges();
            ViewBag.Message = "Data Insert Successfully";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            auth data = db.auths.Find(id);
            if (data == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            else { 
            /*ViewBag.id = id;*/
            return View(data);
            }
        }

        [HttpPost, ActionName("Delete")]

        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            auth data = db.auths.Find(id);
            
            db.auths.Remove(data);
            db.SaveChanges();

            return RedirectToAction("Index");

        }
       

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            auth data = db.auths.Find(id);
            if (data == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            else
            {
                /*ViewBag.id = id;*/
                return View(data);
            }

        }

        [HttpPost]
        public ActionResult Edit(FormCollection fc)
        {
            auth udata = new auth();
            udata.id = int.Parse(Request.Form["id"]);
            udata.fullname = Request.Form["fullname"];
            udata.email = Request.Form["email"];
            udata.password = Request.Form["password"];
            udata.phone = Request.Form["phone"];
            db.Entry(udata).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

    }
}