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
    public class ReceiptsController : Controller
    {
        private readonly PandaDbContext dbContext;

        public ReceiptsController(PandaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        // GET: ReceiptsController
        public ActionResult Index()
        {
            var receipts = this.dbContext.Receipts.Select(r => new ReceiptViewModel
            {
                Id=r.Id,
                Fee = r.Fee,
                IssuedOn = r.IssuedOn,
                RecipientId = r.RecipientId,
                Recipient = r.Recipient,
               
            }).ToList();
            return View(receipts);
        }

        // GET: ReceiptsController/Details/5
    //    public ActionResult Details(int id)
    //    {
    //        return View();
    //    }

    //    // GET: ReceiptsController/Create
    //    public ActionResult Create()
    //    {
    //        return View();
    //    }

   
    }
}
