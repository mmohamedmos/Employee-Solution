using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stack.Entities.Models
{
   public class Employee: IdentityUser
    {
       
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName { get; set; }
        public byte[] ProfilePhoto { get; set; }
        public bool IsMarried { get; set; }
        public virtual EmployeePrice EmployeePrice { get; set; }
    }
}
