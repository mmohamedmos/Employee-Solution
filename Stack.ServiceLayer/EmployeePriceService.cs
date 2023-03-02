using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Stack.Core;
using Stack.DTOs;
using Stack.DTOs.Models;
using Stack.Entities.Models;
using System.IO;
using Stack.DTOs.Dtos;

namespace Stack.ServiceLayer
{
    public class EmployeePriceService
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IConfiguration config;
        private readonly IMapper mapper;

        public EmployeePriceService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;

            this.config = config;
        }
        
        public async Task<ApiResponse<bool>> UpdatePrice(EditPriceDto model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var GetEmp = (await unitOfWork.EmployeePrice.GetAsync(filter: a => a.EmployeeId == model.EmployeeId, includeProperties: "Employee")).ToList().FirstOrDefault();

                mapper.Map(model, GetEmp);

                var EmployeePriceResult = await unitOfWork.EmployeePrice.UpdateAsync(GetEmp);

                if (EmployeePriceResult!=null)
                {

                    await unitOfWork.SaveChangesAsync();
                    result.Succeeded = true;
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                return result;
            }

        }
    }
}
