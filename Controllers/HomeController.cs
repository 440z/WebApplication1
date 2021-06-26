using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //FileUpload to folder
        //***************

        //Fehlermeldung:
        //    Für folgende Webadresse wurde keine Webseite gefunden: https://localhost:44349/Home/UploadFile

        private const string uploadPath = "UploadFolder";

        // In dem Beispiel aus dem Unterricht (Upload/Download) klappt das so
        //private const string uploadPath = @"C:\Users\Windows10\OneDrive - IT-Akademie Dr. Heuer GmbH\git\WebApplication1\UploadFolder\";

        [HttpPost]
        public IActionResult UploadFile(IFormFile dieDatei)
        {
            if (dieDatei == null || dieDatei.Length == 0)
            {
                return Content("You did not choose a file or the file is empty");
            }

            string path = Path.Combine(Directory.GetCurrentDirectory(), uploadPath, Path.GetFileName(dieDatei.FileName));

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                dieDatei.CopyTo(stream);
            }

            DirectoryInfo di = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), uploadPath));
            FileInfo[] files = di.GetFiles();

            return RedirectToAction("ShowFiles");

        }

        public IActionResult ShowFiles()
        {
            DirectoryInfo di = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), uploadPath));

            FileInfo[] files = di.GetFiles();

            //Fehlermeldung:
            //InvalidOperationException: The view 'ShowFiles' was not found. The following locations were searched: /Views/Home/ShowFiles.cshtml /Views/Shared/ShowFiles.cshtml
            return View(files);
        }
    }
}
