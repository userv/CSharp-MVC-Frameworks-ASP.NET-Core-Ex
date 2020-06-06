using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PANDA.Models;

namespace PANDA.ViewModels
{
    public class PackageDetailsViewModel
    {
        
        public string Description { get; set; }
        public float Weight { get; set; }
        public string ShippingAddress { get; set; }
        public string Status { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public string Recipient { get; set; }
    }
}
