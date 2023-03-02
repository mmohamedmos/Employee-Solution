using Stack.DTOs.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Dtos
{
  public  class RowEmployeeDto :EmployeeDTO

    {
        public string Id { get; set; }
        public string FullName { get; set; }

    }
}
