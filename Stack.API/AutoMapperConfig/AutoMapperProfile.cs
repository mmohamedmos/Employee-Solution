using AutoMapper;
using Stack.API.Extensions;
using Stack.DTOs.Dtos;
using Stack.DTOs.Models;
using Stack.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stack.API.AutoMapperConfig
{
    public class AutoMapperProfile : Profile
    {
        //Auto Mapper Configuration File . 
        public AutoMapperProfile()
        {

            //Mirror mapping between an entity and it's DTO . 
            CreateMap<CreateEmployeeDto, Employee>()
                .ForMember(c => c.ProfilePhoto, o => o.MapFrom(s => s.ProfilePhoto.GetImageContent()))
                .ForMember(c => c.EmployeePrice, o => o.MapFrom(s =>new EmployeePrice {Price=s.Price}))
                .ForMember(c => c.UserName, o => o.MapFrom(s =>s.FirstName))
                ;

            CreateMap<EditEmployeeDto, Employee>()
                .ForMember(c => c.ProfilePhoto, o => o.MapFrom(s => s.ProfilePhoto.GetImageContent()))
                .ForPath(c => c.EmployeePrice.Price, o => o.MapFrom(s => s.Price)
                )
                .ForMember(c => c.UserName, o => o.MapFrom(s => s.FirstName)).ReverseMap();

                 ;

            CreateMap<Employee, RowEmployeeDto>()
                  .ForMember(c => c.Price, o => o.MapFrom(s => s.EmployeePrice.Price))
                 ;


            CreateMap<Employee, ViewEmployeeDto>()
                  .ForMember(c => c.Price, o => o.MapFrom(s => s.EmployeePrice.Price))
                  .ForMember(c => c.ProfilePhoto, o => o.MapFrom(s => s.ProfilePhoto)).ReverseMap();
             ;

            CreateMap<EditPriceDto, EmployeePrice>()
                .ForMember(c => c.Price, o => o.MapFrom(s => s.Price))
                .ForMember(c => c.EmployeeId, o => o.MapFrom(s => s.EmployeeId)).ReverseMap();
            ;
        }
    }
}
