using Microsoft.AspNetCore.Http;
using Stack.DTOs.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DTOs.Dtos
{
   public class CreateEmployeeDto : EmployeeDTO
    {  
     public  IFormFile ProfilePhoto { set; get; }
    }
}
