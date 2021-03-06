﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PANDA.Data;
using PANDA.Models;
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

        public IActionResult Details(string id)
        {
            Package package = this.dbContext.Packages
                .Include(p => p.Status)
                .Include(p => p.Recipient)
                .SingleOrDefault(p => p.Id == id);
            if (package == null) return this.Redirect("/Home/Index");
            PackageDetailsViewModel packageDetails = new PackageDetailsViewModel
            {
                Status = package.Status.Name,
                ShippingAddress = package.ShippingAddress,
                Recipient = package.Recipient.UserName,
                Description = package.Description,
                EstimatedDeliveryDate = package.EstimatedDeliveryDate,
                Weight = package.Weight
            };
            return this.View(packageDetails);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var recipients = this.dbContext.Users
                .Select(x => new RecipientDropDownModel { Id = x.Id, FullName = x.UserName }).ToList();
            var viewModel = new PackageCreateInputModel
            {
                Recipients = recipients
            };
            return this.View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PackageCreateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }
            var package = new Package
            {
                Description = inputModel.Description,
                Weight = inputModel.Weight,
                ShippingAddress = inputModel.ShippingAddress,
                Status = this.dbContext.PackageStatuses.FirstOrDefault(x => x.Name == "Pending"),
                RecipientId = inputModel.RecipientId
            };
            this.dbContext.Packages.Add(package);
            this.dbContext.SaveChanges();

            //TODO Redirect to details view
            return this.Redirect("/Home/Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Pending()
        {
            var pendingPackages = this.dbContext.Packages
                .Where(p => p.Status.Name == "Pending")
                .Select(p => new PackagePendingViewModel
                {
                    Id = p.Id,
                    Description = p.Description,
                    Status = p.Status,
                    Recipient = p.Recipient,
                    ShippingAddress = p.ShippingAddress,
                    Weight = p.Weight,
                }).ToList();

            return this.View(pendingPackages);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Ship(string id)
        {
            var shippedPackage = this.dbContext.Packages.Find(id);
            shippedPackage.Status = this.dbContext.PackageStatuses.FirstOrDefault(x => x.Name == "Shipped");
            shippedPackage.EstimatedDeliveryDate = DateTime.UtcNow.AddDays(new Random().Next(20, 40));
            this.dbContext.Packages.Update(shippedPackage);
            this.dbContext.SaveChanges();
            return this.RedirectToAction("Shipped");
        }
        public IActionResult Shipped()
        {
            var shippedPackages = this.dbContext.Packages
                .Where(p => p.Status.Name == "Shipped")
                .Select(p => new PackageShippedViewModel
                {
                    Id = p.Id,
                    Description = p.Description,
                    EstimatedDeliveryDate = p.EstimatedDeliveryDate,
                    Recipient = p.Recipient,
                    Weight = p.Weight,

                }).ToList();
            return this.View(shippedPackages);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Deliver(string id)
        {
            var deliveredPackage = this.dbContext.Packages.Find(id);
            deliveredPackage.Status = this.dbContext.PackageStatuses.FirstOrDefault(x => x.Name == "Delivered");
            this.dbContext.Packages.Update(deliveredPackage);
            this.dbContext.SaveChanges();
            return this.RedirectToAction("Delivered");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delivered(string id)
        {
            var deliveredPackages = this.dbContext.Packages
                .Where(p => p.Status.Name == "Delivered")
                .Select(p => new PackageDeliveredViewModel
                {
                    Id = p.Id,
                    Description = p.Description,
                    EstimatedDeliveryDate = p.EstimatedDeliveryDate,
                    Recipient = p.Recipient,
                    Weight = p.Weight,

                }).ToList();
            return this.View(deliveredPackages);
        }

        public IActionResult Acquire(string id)
        {
            var acquiredPackage = this.dbContext.Packages.Find(id);
            acquiredPackage.Status = this.dbContext.PackageStatuses.FirstOrDefault(x => x.Name == "Acquired");
            this.dbContext.Packages.Update(acquiredPackage);
            this.dbContext.SaveChanges();
            return this.RedirectToAction("Create", "Receipts", new { id });
        }
    }
}