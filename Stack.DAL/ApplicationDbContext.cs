
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stack.Entities;
using Stack.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stack.DAL
{
    //ApplicationDbContext inherits from IdentityDbContext to implement Identity Tables . 
    //Reference the user class that inherits from IdentityUser class, Ex below : "ApplicationUser" .
    public class ApplicationDbContext : IdentityDbContext<Employee>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<EmployeePrice> EmployeePrice { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>()
            .Property(p => p.FullName)
            .HasComputedColumnSql("[FirstName] + ' ' + [LastName]");
        }
      

    }
    
}
