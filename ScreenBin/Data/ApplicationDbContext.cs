using Microsoft.EntityFrameworkCore;
using ScreenBin.Areas.Screenshots.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScreenBin.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Screenshot> Screenshots { get; set; }

    }
}
