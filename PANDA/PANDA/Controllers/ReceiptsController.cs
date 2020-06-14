using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PANDA.Data;
using PANDA.Models;
using PANDA.ViewModels;

namespace PANDA.Controllers
{
    public class ReceiptsController : Controller
    {
        private readonly PandaDbContext dbContext;
        private readonly UserManager<PandaUser> userManager;

        public ReceiptsController(PandaDbContext dbContext, UserManager<PandaUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        // GET: ReceiptsController

        public IActionResult Index()
        {
            var receipts = this.dbContext.Receipts
                .Where(r => r.RecipientId == this.userManager.GetUserId(this.User))
                .Select(r => new ReceiptViewModel
                {
                    Id = r.Id,
                    Fee = r.Fee,
                    IssuedOn = r.IssuedOn,
                    RecipientId = r.RecipientId,
                    Recipient = r.Recipient,

                }).ToList();
            return this.View(receipts);
        }


        public IActionResult Details(string id)
        {
            var receipt = this.dbContext.Receipts
                .Include(r => r.Recipient)
                .Include(p => p.Package)
                .SingleOrDefault(r => r.Id == id);

            var receiptDetail = new ReceiptDetailsViewModel
            {
                Id = id,
                Fee = receipt.Fee,
                IssuedOn = receipt.IssuedOn,
                Package = receipt.Package,
                Recipient = receipt.Recipient,
                ShippingAddress = receipt.Package.ShippingAddress,
            };
            return this.View(receiptDetail);
        }


        public IActionResult Create(string id)
        {
            var acquiredPackage = this.dbContext.Packages.Find(id);
            var receipt = new Receipt
            {
                Fee = (decimal)acquiredPackage.Weight * 2.67m,
                IssuedOn = DateTime.UtcNow,
                PackageId = acquiredPackage.Id,
                Package = acquiredPackage,
                RecipientId = acquiredPackage.RecipientId,
                Recipient = acquiredPackage.Recipient,
            };
            this.dbContext.Receipts.Add(receipt);
            this.dbContext.SaveChanges();
            return this.RedirectToAction("Details",new {id=receipt.Id});
        }


    }
}
