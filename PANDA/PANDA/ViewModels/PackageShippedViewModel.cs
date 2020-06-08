using System;
using System.Runtime.CompilerServices;
using PANDA.Models;

namespace PANDA.ViewModels
{
    public class PackageShippedViewModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public float Weight { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public string RecipientId { get; set; }
        public PandaUser Recipient { get; set; }
    }
}