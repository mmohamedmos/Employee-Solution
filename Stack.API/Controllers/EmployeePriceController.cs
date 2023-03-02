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
    public class EmployeePriceController : BaseResultHandlerController<EmployeePriceService>
    {
        public EmployeePriceController(EmployeePriceService _service) : base(_service)
        {


        }
        [AllowAnonymous]
        [HttpPost("UpdatePrice")]
        public async Task<IActionResult> UpdatePrice(EditPriceDto model)
        {
            return await AddItemResponseHandler(async () => await service.UpdatePrice(model));
        }
    }
}
