using CPT.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CPT.Persistence.Context
{
    public class CPTDbContext : IdentityDbContext<ApplicationUser>
    {
        public CPTDbContext(DbContextOptions<CPTDbContext> options)
          : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<BiometricSetup> BiometricSetup { get; set; }
    }
}
