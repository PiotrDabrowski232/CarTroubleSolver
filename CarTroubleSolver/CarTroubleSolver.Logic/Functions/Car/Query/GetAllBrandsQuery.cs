using MediatR;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTroubleSolver.Logic.Functions.Car.Query
{
    public class GetAllBrandsQuery : IRequest<List<string>>
    {
    }
}
