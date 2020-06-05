using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PANDA.Models;

namespace PANDA.ViewModels
{
    public class ReceiptViewModel
    {
        public string Id { get; set; }
        public decimal Fee { get; set; }

        public DateTime IssuedOn { get; set; } = DateTime.UtcNow;

        public string RecipientId { get; set; }
        public PandaUser Recipient { get; set; }
    }
}
