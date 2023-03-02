using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Models
{
  public class EmployeeDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Price { set; get; }
        public bool IsMarried { get; set; }
    }
}
