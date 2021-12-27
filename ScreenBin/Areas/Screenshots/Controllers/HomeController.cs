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

    public class HomeController : Controller
    {
        public readonly ApplicationDbContext dbContext;

        public HomeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [Route("view/{id}")]
        public IActionResult Details(string id)
        {
            Screenshot screenshot = dbContext.Screenshots.Where(x => x.Id == id).FirstOrDefault();
            if(screenshot == null)
            {
               return new NotFoundResult();
            }
            ViewModels.ScreenshotDetailsViewModel viewModel = new ViewModels.ScreenshotDetailsViewModel()
            {
                Id=screenshot.Id,
                Title = screenshot.Title,
                ImageUri = new Uri($"https://cdn.allsve.com/images/screenbin/{screenshot.RelativePath}")
            };

            if (HttpContext.Session.Keys.Contains(screenshot.Id))
            {
                viewModel.CanEdit = true;
            }
            return View(viewModel);
        }
    }
}
