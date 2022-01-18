using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace CRUD.Controllers
{
    public class AccountController : Controller
    {
        authLoginEntities db = new authLoginEntities();
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            if(Session["id"] == null) { 
            return View();
            }
            else
            {
                return RedirectToAction("Dashboard");
            }
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var checkAuth = db.auths.FirstOrDefault(u => u.email == email && u.password == password);
            if (checkAuth != null)
            {
                Session["fullname"] = checkAuth.fullname;
                Session["email"] = checkAuth.email;
                Session["id"] = checkAuth.id;
                Session["picture"] = checkAuth.profilepic;

                return RedirectToAction("Dashboard");
            }
            else if (checkAuth == null)
            {
                ViewBag.error = "sorry invalid login credentials";
                return View();
            }

                return View();
        }

        public ActionResult Register() {

            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection fc, HttpPostedFileBase profilepic)
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

        public ActionResult Dashboard()
        {
            if(Session["id"] != null)
            {
                return View();
            }
            else
            {
               return  RedirectToAction("Login");
            }
            
        }


        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}