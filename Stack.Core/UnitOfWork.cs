using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stack.Core.Managers;
using Stack.DAL;
using Stack.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stack.Core
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext context;
        public UnitOfWork(ApplicationDbContext context, EmployeeRepositry employee, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            Employee = employee;
            RoleManager = roleManager;
        }
        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return await context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // log message and enteries
            }
            catch (DbUpdateException ex)
            {
                // log message and enteries
            }
            catch (Exception ex)
            {
                // Log here.
            }
            return false;
        }
        public EmployeeRepositry Employee { get; private set; } //Manager for application users table . 
        public RoleManager<IdentityRole> RoleManager { get; private set; } //Manager for application user roles table . 


        private EmployeePriceRepositry employeePrice;
        public EmployeePriceRepositry EmployeePrice
        {
            get
            {
                if (employeePrice == null)
                {
                    employeePrice = new EmployeePriceRepositry(context);
                }
                return employeePrice;
            }
        }



      
        
    }
}
