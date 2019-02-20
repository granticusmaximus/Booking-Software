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
    [Route("api/General")]
    public class GeneralController : Controller
    {
        private readonly IFunctional _functionalService;
        private readonly INumberSequence _numberSequence;

        public GeneralController(IFunctional functionalService,
            INumberSequence numberSequence)
        {
            _functionalService = functionalService;
            _numberSequence = numberSequence;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<General> Items = _functionalService.GetList<General>().ToList();
            int Count = Items.Count();
            return Ok(new { Items, Count });

        }

        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<General> payload)
        {
            General value = payload.value;
            value.GeneralName = _numberSequence.GetNumberSequence("GEN");
            var result = _functionalService.Insert<General>(value);
            value = (General)result.Data;
            return Ok(value);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<GeneralViewModel> payload)
        {
            GeneralViewModel value = payload.value;
            var result = _functionalService
                .Update<GeneralViewModel, General>(value, Convert.ToInt32(value.GeneralId));
            return Ok();
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<General> payload)
        {
            var result = _functionalService.Delete<General>(Convert.ToInt32(payload.key));
            return Ok();

        }
    }
}