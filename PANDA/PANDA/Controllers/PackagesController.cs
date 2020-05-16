using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PANDA.Data;
using PANDA.ViewModels;

namespace PANDA.Controllers
{
    public class PackagesController : Controller
    {
        private readonly PandaDbContext dbContext;

        public PackagesController(PandaDbContext dbContext)
        {
            this.dbContext = dbContext;

        }

        // GET: Packages
        public ActionResult Index()
        {
            return View();
        }

        // GET: Packages/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Packages/Create
        public ActionResult Create()
        {
            var recipients = this.dbContext.Users
                .Select(x => new RecipientDropDownModel() { Id = x.Id, FullName = x.UserName }).ToList();
            
            var viewModel=new PackageCreateInputModel
            {
                Recipients = recipients
            };
            return View(viewModel);
        }

        // POST: Packages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PackageCreateInputModel inputModel)
        {
            

            return View();

        }

        // GET: Packages/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Packages/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Packages/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Packages/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}