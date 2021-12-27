using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScreenBin.Areas.Screenshots.ViewModels
{
    public class ScreenshotDetailsViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public Uri ImageUri { get; set; }

        public bool CanEdit { get; set; } = false;
    }
}
