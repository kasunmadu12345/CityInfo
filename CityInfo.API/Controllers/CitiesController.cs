using CityInfo.API.Entities;
using CityInfo.API.Models;
using CityInfo.API.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using CityInfo.API.Context;
using CityInfo.API.Entities;
using CityInfo.API.Models;
using CityInfo.API.Services.Contract;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")] // Matches the URL format you provided
    public class CitiesController : Controller
    {
        private readonly ILocalMailService _localMailService;
        private readonly CityInfoContext _cityInfoContext;


        private List<CityDto> _cities = new List<CityDto>
        {
            new CityDto { Id = 1, Name = "New York", Description = "The Big Apple" },
            new CityDto { Id = 2, Name = "London", Description = "The Big Smoke" },
            new CityDto { Id = 3, Name = "Tokyo", Description = "The Mega City" },
            new CityDto { Id = 4, Name = "Paris", Description = "The City of Love" },
            new CityDto { Id = 5, Name = "Sydney", Description = "The Harbour City" }
        };

        public CitiesController(CityInfoContext cityInfoContext, ILocalMailService localMailService)
        {
            _localMailService = localMailService;
            _cityInfoContext = cityInfoContext;
        }

        [HttpGet]
        //public ActionResult<IEnumerable<CityDto>> GetCities()
        public ActionResult<IEnumerable<City>> GetCities()
        {
            if (Request.Headers.TryGetValue("Accept", out var acceptHeaderValue))
            {
                if (acceptHeaderValue.Contains("application/json"))
                {
                    var cities = _cityInfoContext.Cities.ToList();
                    //return Ok(_cities);
                    return Ok(cities);
                }
                else if (acceptHeaderValue.Contains("application/XML"))
                {
                    var cities = _cityInfoContext.Cities.ToList();
                    return Ok(new CitiesDto { Cities = cities });
                }
            }

            return BadRequest("Unsupported media type");
        }

        [HttpGet("GetCity/{id}")]
        public IActionResult GetCity(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = _cities.FirstOrDefault(c => c.Id == id);
            if (city == null)
            {
                return NotFound();
            }
            _localMailService.SendMail("abc@pqr.com", "xyz abc", "test mail");
            return Ok(city);
        }
    }
}
