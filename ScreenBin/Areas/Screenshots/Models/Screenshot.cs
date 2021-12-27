using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScreenBin.Areas.Screenshots.Models
{
    public class Screenshot
    {
        [MaxLength(191)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [MaxLength(255)]
        public string Title { get; set; } = "Unnamed";
        [MaxLength(300)]
        public string RelativePath { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.Now;
    }
}
