using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PANDA.Models;

namespace PANDA.ViewModels
{
    public class PackagePendingViewModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public float Weight { get; set; }
        public string ShippingAddress { get; set; }
        public string PackageStatusId { get; set; }
        public PackageStatus Status { get; set; }
        public string RecipientId { get; set; }
        public PandaUser Recipient { get; set; }
    }
}
