using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.Entities.Models
{
   public class EmployeePrice
    {
        public int Id { get; set; }
        public double Price{ get; set; }
        public string EmployeeId { get; set; }
        public  virtual Employee Employee { set; get; }
    }
}
