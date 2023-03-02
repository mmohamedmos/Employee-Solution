using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Dtos;
using Stack.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : BaseResultHandlerController<EmployeeService>
    {
        public EmployeeController(EmployeeService _service) : base(_service)
        {


        }
        [AllowAnonymous]
        [HttpGet("GetAllEmployee")]
        public async Task<IActionResult> GetAllEmployee()
        {
            return await AddItemResponseHandler(async () => await service.GetAllEmployees());
        }
        [AllowAnonymous]
        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee([FromForm]CreateEmployeeDto model)
        {
            return await AddItemResponseHandler(async () => await service.CreateEmployee(model));
        }
        [AllowAnonymous]
        [HttpPost("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromForm]EditEmployeeDto model)
        {
            return await AddItemResponseHandler(async () => await service.EditEmployee(model));
        }
        [AllowAnonymous]
        [HttpDelete("DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            return await AddItemResponseHandler(async () => await service.DeleteEmployee(id));
        }


        [AllowAnonymous]
        [HttpGet("GetEmployee/{id}")]
        public async Task<IActionResult> GetEmployee(string id)
        {
            return await AddItemResponseHandler(async () => await service.ViewEmployee(id));
        }


    }
}

    
