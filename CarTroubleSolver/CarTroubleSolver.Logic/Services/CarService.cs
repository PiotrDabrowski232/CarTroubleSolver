using AutoMapper;
using CarTroubleSolver.Api.Authentication;
using CarTroubleSolver.Data.Models;
using CarTroubleSolver.Data.Repositories.Interfaces;
using CarTroubleSolver.Logic.Dto.User;
using CarTroubleSolver.Logic.Exceptions;
using CarTroubleSolver.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CarTroubleSolver.Logic.Services
{
    public class CarService: ICarService
    {
       
    }
}
