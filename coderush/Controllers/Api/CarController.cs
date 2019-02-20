using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using coderush.Data;
using coderush.Models;
using coderush.Models.SyncfusionViewModels;
using Microsoft.AspNetCore.Authorization;
using coderush.Services;

namespace coderush.Controllers.Api
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Car")]
    public class CarController : Controller
    {
        private readonly IFunctional _functionalService;
        private readonly INumberSequence _numberSequence;

        public CarController(IFunctional functionalService,
            INumberSequence numberSequence)
        {
            _functionalService = functionalService;
            _numberSequence = numberSequence;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Car> Items = _functionalService.GetList<Car>().ToList();
            int Count = Items.Count();
            return Ok(new { Items, Count });

        }

        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<Car> payload)
        {
            Car value = payload.value;
            value.CarName = _numberSequence.GetNumberSequence("CAR");
            var result = _functionalService.Insert<Car>(value);
            value = (Car)result.Data;
            return Ok(value);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<CarViewModel> payload)
        {
            CarViewModel value = payload.value;
            var result = _functionalService
                .Update<CarViewModel, Car>(value, Convert.ToInt32(value.CarId));
            return Ok();
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<Car> payload)
        {
            var result = _functionalService.Delete<Car>(Convert.ToInt32(payload.key));
            return Ok();

        }
    }
}