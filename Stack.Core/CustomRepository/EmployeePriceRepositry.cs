using Stack.DAL;
using Stack.Entities;
using Stack.Entities.Models;
using Stack.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.Core.Managers
{
   public class EmployeePriceRepositry : Repository<EmployeePrice, ApplicationDbContext>
    {
        public EmployeePriceRepositry(ApplicationDbContext _contect) : base(_contect)
        {

        }
    }

}
