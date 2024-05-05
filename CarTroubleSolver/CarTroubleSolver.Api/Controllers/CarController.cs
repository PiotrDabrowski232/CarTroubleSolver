﻿using CarTroubleSolver.Logic.Services.Interfaces;
using CarTroubleSolver.Logic.Functions.Car.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarTroubleSolver.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarController(IMediator mediator, ICarService carService) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ICarService _carService = carService;

        [HttpGet]
        [Route("/GetCarData")]
        public async Task<IActionResult> GetCarSpecification()
        {
            try
            {

                var result = _mediator.Send(new GetAllBrandsQuery());
                return Ok(result);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
