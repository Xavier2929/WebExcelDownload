using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebExcelDownload.Controllers
{
    public class FileUploadController : Controller
    {
        // GET: FileUpload
        public ActionResult Index()
        {
            var items = GetFiles();
            return View(items);
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            //aqui empieza el metodo de guardado, hacemos una sobrecarga con el metodo Index
            if (file == null && file.ContentLength<0)
            {
                ViewBag.Message = "No haz metido ningun archivo";
                
            }
            try
            {
                string path = Path.Combine(Server.MapPath("~/Archives"),
                    Path.GetFileName(file.FileName));

                file.SaveAs(path);
                ViewBag.Message = "Archivo se ha subido exitosamente!";
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: "+ex.Message;
                
            }
            var items = GetFiles();
            return View(items);
        }

        public FileResult Download(string fileName)
        {
            var fileVirtualPath = "~/Archives/" + fileName;
            return File(fileVirtualPath,"application/force- download",Path.GetFileName(fileVirtualPath));
        }
        private List<string> GetFiles()
        {
            List<string> files = new List<string>();
            var dir = new System.IO.DirectoryInfo(Server.MapPath("~/Archives"));
            System.IO.FileInfo[] fileNames = dir.GetFiles("*.*"); //me devuelve todo tipo de archivos
            foreach (var file in fileNames)
            {
                files.Add(file.Name);
            }
            return files;
        }


     
    }
}