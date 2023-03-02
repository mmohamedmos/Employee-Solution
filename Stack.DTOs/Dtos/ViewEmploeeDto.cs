using Stack.DTOs.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Dtos
{
  public  class ViewEmployeeDto:EmployeeDTO
    {
        public string FullName { get; set; }
        public string Id { get; set; }
        public bool HasProfilePhoto { get; set; }
        public byte[] ProfilePhoto { get; set; }



    }
}
