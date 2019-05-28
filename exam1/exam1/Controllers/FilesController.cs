using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using exam1.Data;
using exam1.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace exam1.Controllers
{
    public class FilesController : Controller
    {
        IHostingEnvironment _appEnvironment;
        ApplicationDbContext _context;

        public FilesController(ApplicationDbContext dbContext,
            IHostingEnvironment appEnvironment)
        {

            _appEnvironment = appEnvironment;
            _context = dbContext;
        }

        public IActionResult Index()
        {
            return View(model: _context.Files.ToList());
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View(model: new Files());
        }

        [HttpPost]
        public async Task<IActionResult> Upload(Files files, IFormFile file)
        {
            if (file != null)
            {
                files.Slug = Guid.NewGuid().ToString() + file.FileName;
                files.PathToFile = "/Files/" + "_" + files.Slug;
                files.FIleName = files.FIleName;
                files.UploadedDate = DateTime.Now;
                files.ContentType = file.ContentType;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + files.Slug, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                _context.Files.Add(files);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var file = _context.Files.FirstOrDefault(x => x.Id == id);
            return View(model: file);
        }

        [HttpGet]
        public async Task<IActionResult> Download(string slug)
        {
            var file = _context.Files.FirstOrDefault(x => x.Slug == slug);
            if (file != null)
            {
                var path = _appEnvironment.WebRootPath + file.PathToFile;
                file.DownloadedCount++;
                await _context.SaveChangesAsync();
                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, file.ContentType, fileDownloadName: file.FIleName);
            }
            return NotFound();
        }
    }
}
