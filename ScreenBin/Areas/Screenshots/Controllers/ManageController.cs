using FluentFTP;
using Microsoft.AspNetCore.Mvc;
using ScreenBin.Areas.Screenshots.Models;
using ScreenBin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScreenBin.Areas.Screenshots.Controllers
{
    [Area("Screenshots")]
    public class ManageController : Controller
    {
        protected readonly ApplicationDbContext dbContext;

        public ManageController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult Update(string id,[Bind("Title")] Screenshot EditData)
        {
            Screenshot screenshot = dbContext.Screenshots.Where(x => x.Id == id).FirstOrDefault();
            if(screenshot == null)
            {
                return new NotFoundResult();
            }
            screenshot.Title = EditData.Title;
            dbContext.SaveChanges();
            return RedirectToAction("Details", "Home", new { area = "Screenshots", id = screenshot.Id });
            return View();
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            Screenshot screenshot = dbContext.Screenshots.Where(x => x.Id == id).FirstOrDefault();
            if (screenshot == null)
            {
                return new NotFoundResult();
            }

            //remove session cookie that is added so uploader can edit picture after upload //
            HttpContext.Session.Remove(screenshot.Id);
            //connect to ftp an delete file //
            FtpClient ftpClient = new FtpClient("allsve.com", new System.Net.NetworkCredential("webview", "adriano10inter"));
            ftpClient.Connect();
            ftpClient.DeleteFile($"/allsve.com/cdn/images/screenbin/{screenshot.RelativePath}");
            ftpClient.Disconnect();
            //delete screenshot record from the db//
            dbContext.Screenshots.Remove(screenshot);
            dbContext.SaveChanges();


            return RedirectToAction("Index", "Home",new { area = "" });



        }
    }
}
