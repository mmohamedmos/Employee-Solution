﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stack.DTOs;

namespace Stack.API.Controllers.Common
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class BaseResultHandlerController<TService> : ControllerBase
    {
        protected readonly TService service;
        public BaseResultHandlerController(TService _service)
        {
            service = _service;
        }

        [NonAction]
        protected async Task<IActionResult> GetResponseHandler<T>(Func<Task<ApiResponse<T>>> serviceMethod)
        {
            try
            {
                var result = await serviceMethod();
                if (result.Succeeded)
                {
                    return Ok(result);
                }
                return NotFound(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [NonAction]
        protected async Task<IActionResult> AddItemResponseHandler<T>(Func<Task<ApiResponse<T>>> serviceMethod)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await serviceMethod();
                    if (result.Succeeded)
                    {
                        return Ok(result);
                    }
                    return BadRequest(result);
                }
                return BadRequest(new ApiResponse<object>()
                {
                    Succeeded = false,
                    Errors = GetModelStateErrors(ModelState).ToList(),
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [NonAction]
        protected async Task<IActionResult> EditItemResponseHandler<T>(Func<Task<ApiResponse<T>>> serviceMethod)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await serviceMethod();
                    if (result.Succeeded)
                    {
                        return Ok(result);
                    }
                    return BadRequest(result);
                }
                return BadRequest(new ApiResponse<object>()
                {
                    Succeeded = false,
                    Errors = GetModelStateErrors(ModelState).ToList(),
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [NonAction]
        protected async Task<IActionResult> RemoveItemResponseHandler<T>(Func<Task<ApiResponse<T>>> serviceMethod)
        {
            try
            {
                var result = await serviceMethod();
                if (result.Succeeded)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        private IEnumerable<string> GetModelStateErrors(ModelStateDictionary modelState)
        {
            foreach (var modelstateEntry in modelState.Values)
            {
                var errors = modelstateEntry.Errors;
                foreach (var error in errors)
                {
                    yield return error.ErrorMessage;
                }
            }
        }

    }
}
