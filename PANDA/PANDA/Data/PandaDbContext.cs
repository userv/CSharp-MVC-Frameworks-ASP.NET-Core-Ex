using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using PANDA.Models;

namespace PANDA.Data
{
    public class PandaDbContext : IdentityDbContext<PandaUser>
    {
        public PandaDbContext(DbContextOptions<PandaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Package> Packages { get; set; }
        public DbSet<Receipt> Receipts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }

    
}
