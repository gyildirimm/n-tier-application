using AutoMapper;
using Core.Entities.Identity;
using DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapping
{
    public class AuthMapper : Profile
    {
        public AuthMapper()
        {
            CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();
        }
    }
}
