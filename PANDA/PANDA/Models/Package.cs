using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PANDA.Models
{
    public class Package
    {
        public Package()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string Description { get; set; }
        public float Weight { get; set; }
        public string ShippingAddress { get; set; }
        public string PackageStatusId { get; set; }
        public PackageStatus Status { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public string RecipientId { get; set; }
        public PandaUser Recipient { get; set; }
    }
}
