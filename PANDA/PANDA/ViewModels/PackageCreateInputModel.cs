using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors.Infrastructure;
using PANDA.Models;

namespace PANDA.ViewModels
{
    public class PackageCreateInputModel
    {
        public string Description { get; set; }

        public float Weight { get; set; }
        
        [Display(Name = "Shipping Address")]
        public string ShippingAddress { get; set; }

        [Display(Name="Recipient")]
        public string RecipientId { get; set; }


        public IEnumerable<RecipientDropDownModel> Recipients { get; set; }
    }
}
