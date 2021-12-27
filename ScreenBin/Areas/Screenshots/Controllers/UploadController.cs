using FluentFTP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScreenBin.Areas.Screenshots.Models;
using ScreenBin.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ScreenBin.Areas.Screenshots.Controllers
{
    [Area("Screenshots")]
    public class UploadController : Controller
    {
        protected readonly ApplicationDbContext dbContext;

        public UploadController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Upload(IFormFile file)
        {
            //do the upload work here//
            FtpClient ftpClient = new FtpClient("allsve.com",new NetworkCredential("webview","adriano10inter"));
            ftpClient.Connect();

            MemoryStream ms = new MemoryStream();

            file.CopyTo(ms);

            Screenshot screenshot = new Screenshot();
            screenshot.Title = file.FileName;
            string filename = screenshot.Id + $"-{file.FileName}";
            string relativePath = $"uploads/{ DateTime.Now.Date.ToString("dd-MM-yyyy")}/{filename}";
            screenshot.RelativePath = relativePath;
            dbContext.Screenshots.Add(screenshot);
            dbContext.SaveChanges();

            ftpClient.Upload(ms,$"/allsve.com/cdn/images/screenbin/{relativePath}",FtpRemoteExists.Overwrite,true);
            //.../
            ftpClient.Disconnect();
            HttpContext.Session.SetString(screenshot.Id, DateTime.Now.ToString());
            return RedirectToAction("Details","Home",new { id = screenshot.Id});
        }
    }
}
