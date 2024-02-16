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
        }
    }
}
