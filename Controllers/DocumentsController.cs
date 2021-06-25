using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly WebApplication1Context _context;

        public DocumentsController(WebApplication1Context context)
        {
            _context = context;
        }

        // GET: Documents
        public async Task<IActionResult> Index()
        {
            return View(await _context.Document.ToListAsync());
        }

        // GET: Documents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Document
                .FirstOrDefaultAsync(m => m.ID == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // GET: Documents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Documents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Length,Author,PublishingDate,DataType")] Document document)
        {
            if (ModelState.IsValid)
            {
                _context.Add(document);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(document);
        }

        // GET: Documents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Document.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Length,Author,PublishingDate,DataType")] Document document)
        {
            if (id != document.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(document);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentExists(document.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(document);
        }

        // GET: Documents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Document
                .FirstOrDefaultAsync(m => m.ID == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var document = await _context.Document.FindAsync(id);
            _context.Document.Remove(document);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentExists(int id)
        {
            return _context.Document.Any(e => e.ID == id);
        }

        ////FileUpload to folder
        ////***************

        ////Fehlermeldung:
        ////    Für folgende Webadresse wurde keine Webseite gefunden: https://localhost:44349/Home/UploadFile

        ////private const string uploadPath = "UploadFolder";

        //// In dem Beispiel aus dem Unterricht (Upload/Download) klappt das so
        //private const string uploadPath = @"C:\Users\Windows10\OneDrive - IT-Akademie Dr. Heuer GmbH\git\WebApplication1\UploadFolder\";






        ////DER PART FEHLT HIER JA!!! ... Die Index von dem Upload!!
        ////     public IActionResult Index()
        ////{
        ////    return View();
        ////}
        //    // ??? Das ganze in den Homecontroller schicken





        //[HttpPost]
        //public IActionResult UploadFile(IFormFile dieDatei)
        //{
        //    if (dieDatei == null || dieDatei.Length == 0)
        //    {
        //        return Content("You did not choose a file or the file is empty");
        //    }

        //    string path = Path.Combine(Directory.GetCurrentDirectory(), uploadPath, Path.GetFileName(dieDatei.FileName));

        //    using (FileStream stream = new FileStream(path, FileMode.Create))
        //    {
        //        dieDatei.CopyTo(stream);
        //    }

        //    DirectoryInfo di = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), uploadPath));
        //    FileInfo[] files = di.GetFiles();

        //    return RedirectToAction("ShowFiles");

        //}

        //public IActionResult ShowFiles()
        //{
        //    DirectoryInfo di = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), uploadPath));

        //    FileInfo[] files = di.GetFiles();

        //    return View(files);
        //}




    }
}
