using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        public ReceiptsController(PandaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        // GET: ReceiptsController
        public IActionResult Index()
        {
            var receipts = this.dbContext.Receipts.Select(r => new ReceiptViewModel
            {
                Id = r.Id,
                Fee = r.Fee,
                IssuedOn = r.IssuedOn,
                RecipientId = r.RecipientId,
                Recipient = r.Recipient,

            }).ToList();
            return View(receipts);
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
            return View(receiptDetail);
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
            return this.View("Details");
        }


    }
}
