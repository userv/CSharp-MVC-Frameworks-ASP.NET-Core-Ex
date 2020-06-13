using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PANDA.Data;
using PANDA.Models;
using PANDA.ViewModels;

namespace PANDA.Controllers
{
    public class HomeController : Controller
    {
        private readonly PandaDbContext dbContext;
        private readonly UserManager<PandaUser> userManager;

        public HomeController(PandaDbContext dbContext, UserManager<PandaUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.View(new List<PackageHomeViewModel> { });
            }
            var userId = this.userManager.GetUserId(this.User);
            var packages = this.dbContext.Packages
                .Where(p => p.RecipientId == userId)
                .Select(p => new PackageHomeViewModel()
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
    }
}
