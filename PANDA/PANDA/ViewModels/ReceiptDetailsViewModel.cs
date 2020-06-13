using System;
using PANDA.Models;

namespace PANDA.ViewModels
{
    public class ReceiptDetailsViewModel
    {
        public string Id { get; set; }
        public decimal Fee { get; set; }
        public DateTime IssuedOn { get; set; }
        public string ShippingAddress { get; set; }
        public PandaUser Recipient { get; set; }
        public Package Package { get; set; }
    }
}
