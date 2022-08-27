using AutoMapper;
using CPT.Domain.Entities;
using CPT.Helper.Dto.Request.Account;
using CPT.Helper.Dto.Response.Account;
using CPT.Helper.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CPT.Infrastructure.MapperConfig
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<RegisterUserRequestDto, ApplicationUser>().ReverseMap();
            CreateMap<TokenGeneratorViewModel, LoginResponseDto>().ReverseMap();
          
            CreateMap<ApplicationUser, ApplicationUserViewModel>().ReverseMap();
        }
    }
}
