using Microsoft.AspNetCore.Http;
using Stack.DTOs.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Dtos
{
  public  class EditEmployeeDto:EmployeeDTO
    {
        public IFormFile ProfilePhoto { set; get; }
        public string Id { get; set; }

    }
}
