using Microsoft.AspNetCore.Mvc;
using RBCTest.Models;
using static System.Net.Mime.MediaTypeNames;

namespace RBCTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly RBCTestContext _sql;
        public HomeController(RBCTestContext sql)
        {
            _sql = sql;
        }

        public IActionResult Index()
        {
            return View(_sql.Files.ToList());
        }
        public IActionResult AddFile()
        {
            return View();
        }
        public IActionResult EditFile(int id)
        {
            Models.File oldFile = _sql.Files.SingleOrDefault(x => x.FileId == id);
            FileDto fileDto= new FileDto();
            fileDto.Name=oldFile.Name;
            fileDto.Description=oldFile.Description;
            fileDto.FileId=oldFile.FileId;
            return View(fileDto);
        }
        public IActionResult DeleteFile(int id)
        {
            _sql.Files.Remove(_sql.Files.SingleOrDefault(x => x.FileId == id));
            _sql.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult AddFile(Models.File file, IFormFile FileName)
        {
            if (FileName != null)
            {
                string filename = Path.GetFileNameWithoutExtension(FileName.FileName) + Path.GetExtension(FileName.FileName);
                using (Stream stream = new FileStream("wwwroot/Images/" + filename, FileMode.Create))
                {
                    FileName.CopyTo(stream);
                }
                file.FileName = filename;
            }
            file.Date = global::System.DateTime.Now;
            _sql.Files.Add(file);
            _sql.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult EditFile(int id, Models.FileDto fileDto)
        {
            
            Models.File oldfile = _sql.Files.SingleOrDefault(x => x.FileId == id);
            if (fileDto.formFile.FileName != null)
            {

                System.IO.File.Delete("wwwroot/Images/" + oldfile.FileName);
                using (Stream stream = new FileStream("wwwroot/Images/" + fileDto.formFile.FileName, FileMode.Create))
                {
                    fileDto.formFile.CopyTo(stream);
                }
            }
            oldfile.Name = fileDto.Name;
            oldfile.Description = fileDto.Description;
            oldfile.FileName=fileDto.formFile.FileName;
            _sql.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}