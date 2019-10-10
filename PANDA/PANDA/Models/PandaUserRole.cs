using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace PANDA.Models
{
    public class PandaUserRole :  IdentityRole
    {
        public PandaUserRole() :
            this(null)
        {
        }

        public PandaUserRole(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
