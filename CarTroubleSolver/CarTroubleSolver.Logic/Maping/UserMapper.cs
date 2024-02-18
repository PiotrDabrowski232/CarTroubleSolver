using AutoMapper;
using CarTroubleSolver.Data.Models;
using CarTroubleSolver.Logic.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTroubleSolver.Logic.Maping
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, RegisterUserDto>();
            CreateMap<RegisterUserDto, User>();


            CreateMap<User, ChangePasswordUserDto>();
            CreateMap<ChangePasswordUserDto, User>();

            CreateMap<User, UpdateUserDto>();
            CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id)));

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();


        }
    }
}
