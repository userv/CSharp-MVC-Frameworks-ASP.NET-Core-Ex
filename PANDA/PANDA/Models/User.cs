using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace PANDA.Models
{
    public sealed class PandaUser : IdentityUser
    {
        public PandaUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Packages=new HashSet<Package>();
            this.Receipts=new HashSet<Receipt>();

            //TODO Remove this later
            //this.Roles = new HashSet<PandaUserRole>();
            //this.Roles.Add(new PandaUserRole(){Name = "Admin", NormalizedName= "ADMIN"});
        }
        public string FullName { get; set; }
        public ICollection<Package> Packages { get; set; }
        public ICollection<Receipt> Receipts { get; set; }
        public PandaUserRole Role { get; set; }
        
    }
}
