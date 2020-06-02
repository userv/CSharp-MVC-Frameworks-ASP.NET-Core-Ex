using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PANDA.Data;
using PANDA.Models;
using PANDA.ViewModels;

namespace PANDA.Controllers
{
    public class HomeController : Controller
    {
        private readonly PandaDbContext dbContext;

        public HomeController(PandaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var packages = this.dbContext.Packages.Select(p => new PackageHomeViewModel()
            {
                Id = p.Id,
                Description = p.Description,
                Status = p.Status.Name
            }).ToList();
            return this.View(packages);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        public IActionResult Test()
        {
            return this.View();
        }
    }
}
