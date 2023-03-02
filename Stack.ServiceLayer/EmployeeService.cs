
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Stack.Core;
using Stack.Core.Managers;
using Stack.DTOs;
using Stack.Entities.Models;
using Stack.ServiceLayer;
using Stack.DTOs.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using System.Web;
using System.Net.Mail;
using System.Net;
using Stack.DTOs.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Stack.ServiceLayer
{
    public class EmployeeService
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IConfiguration config;
        private readonly IMapper mapper;

        public EmployeeService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.config = config;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<bool>> CreateRole(string roleName)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                bool x = await unitOfWork.RoleManager.RoleExistsAsync(roleName);
                if (!x)
                {
                    var role = new IdentityRole();
                    role.Name = roleName;

                    var res = await unitOfWork.RoleManager.CreateAsync(role);
                  

                    if (res.Succeeded)
                    {
                        result.Data = true;
                        result.Succeeded = true;
                        return result;
                    }
                    result.Succeeded = false;
                    foreach (var error in res.Errors)
                    {
                        result.Errors.Add(error.Description);
                    }
                    return result;
                }
                result.Succeeded = false;
                result.Errors.Add("Unable to create role !");
                return result;
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                return result;
            }

        }

        public async Task<ApiResponse<bool>> CreateEmployee(CreateEmployeeDto model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                await CreateRole("Employee");

                Employee emp1= mapper.Map<Employee>(model);

                var createUserResult = await unitOfWork.Employee.CreateAsync(emp1);
                

                if (createUserResult.Succeeded)
                {
                    await unitOfWork.SaveChangesAsync();
                    result.Succeeded = true;
                    return result;

                }
                else
                {
                    result.Succeeded = false;
                    foreach (var item in createUserResult.Errors)
                    {
                        result.Errors.Add(item.Description);
                    }
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

        public async Task<ApiResponse<bool>> EditEmployee(EditEmployeeDto model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var emp = await unitOfWork.Employee.Users.Include(a=>a.EmployeePrice).FirstOrDefaultAsync(a=>a.Id==model.Id);

                 mapper.Map(model,emp);
                 
                var UpdateUserResult = await unitOfWork.Employee.UpdateAsync(emp);


                if (UpdateUserResult.Succeeded)
                {

                    await unitOfWork.SaveChangesAsync();
                    result.Succeeded = true;
                    result.Data = true;
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    foreach (var item in UpdateUserResult.Errors)
                    {
                        result.Errors.Add(item.Description);
                    }
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

        public async Task<ApiResponse<List<RowEmployeeDto>>> GetAllEmployees()
        {
            ApiResponse<List<RowEmployeeDto>> result = new ApiResponse<List<RowEmployeeDto>>();
            try
            {


                var UpdateUserResult =await unitOfWork.Employee.Users.Include(a=>a.EmployeePrice).ToListAsync();


                if (UpdateUserResult!=null)
                {
                    List<RowEmployeeDto> emps = mapper.Map<List<RowEmployeeDto>>(UpdateUserResult);


                        result.Data = emps;
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

        public async Task<ApiResponse<ViewEmployeeDto>> ViewEmployee(string Id)
        {
            ApiResponse<ViewEmployeeDto> result = new ApiResponse<ViewEmployeeDto>();
            try
            {


                Employee EmployeeResult = await unitOfWork.Employee.Users.Include(a=>a.EmployeePrice).FirstOrDefaultAsync();


                if (EmployeeResult!=null)
                {
                   ViewEmployeeDto Employee = mapper.Map<ViewEmployeeDto>(EmployeeResult);
                    result.Data = Employee;
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

        public async Task<ApiResponse<bool>> DeleteEmployee(string Id)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var emp = await unitOfWork.Employee.Users.Include(a => a.EmployeePrice).FirstOrDefaultAsync(a => a.Id == Id);


                var DeleteResult = await unitOfWork.Employee.DeleteAsync(emp);



                if (DeleteResult.Succeeded)
                {
                    await unitOfWork.SaveChangesAsync();

                    result.Succeeded = true;
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    foreach (var item in DeleteResult.Errors)
                    {
                        result.Errors.Add(item.Description);
                    }
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


