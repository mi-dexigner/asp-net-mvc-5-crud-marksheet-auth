using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace CRUD.Controllers
{
    public class ImageController : Controller
    {
        
        authLoginEntities db = new authLoginEntities();
        // GET: Image
        public ActionResult Index()
        {
            var data = db.mediaImages.ToList();

            return View(data);
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase img)
        {
            /*var ex = Path.GetExtension(img.FileName);
            var path = Path.GetFileNameWithoutExtension(img.FileName);

            img.SaveAs(@"D:\sandbox\c#\CRUD\CRUD\uploads\" + path + ex);*/
            try
            {
                mediaImage data = new mediaImage();
                if (img.ContentLength > 0)
                {
                    string _FileName = Path.GetFileNameWithoutExtension(img.FileName);
                    string _FileExe = Path.GetExtension(img.FileName);
                    Random rnd = new Random();
                    var imgSave = _FileName + rnd.Next() + _FileExe;
                    string _path = Path.Combine(Server.MapPath("~/uploads"), imgSave);
                    img.SaveAs(_path);

                    data.FileName = imgSave;
                    db.mediaImages.Add(data);
                    db.SaveChanges();
                    ViewBag.Img = imgSave;
                }
                ViewBag.Message = "File uploaded successfully";
                return View();
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }

        }

    }
}