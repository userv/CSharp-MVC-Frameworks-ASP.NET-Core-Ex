using System;
using PANDA.Models;

namespace PANDA.ViewModels
{
    public class PackageDeliveredViewModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public float Weight { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public string RecipientId { get; set; }
        public PandaUser Recipient { get; set; }
    }
}