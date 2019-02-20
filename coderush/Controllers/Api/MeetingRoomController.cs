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
    [Route("api/MeetingRoom")]
    public class MeetingRoomController : Controller
    {
        private readonly IFunctional _functionalService;
        private readonly INumberSequence _numberSequence;

        public MeetingRoomController(IFunctional functionalService,
            INumberSequence numberSequence)
        {
            _functionalService = functionalService;
            _numberSequence = numberSequence;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<MeetingRoom> Items = _functionalService.GetList<MeetingRoom>().ToList();
            int Count = Items.Count();
            return Ok(new { Items, Count });

        }

        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<MeetingRoom> payload)
        {
            MeetingRoom value = payload.value;
            value.MeetingRoomName = _numberSequence.GetNumberSequence("ROOM");
            var result = _functionalService.Insert<MeetingRoom>(value);
            value = (MeetingRoom)result.Data;
            return Ok(value);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<MeetingRoomViewModel> payload)
        {
            MeetingRoomViewModel value = payload.value;
            var result = _functionalService
                .Update<MeetingRoomViewModel, MeetingRoom>(value, Convert.ToInt32(value.MeetingRoomId));
            return Ok();
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<MeetingRoom> payload)
        {
            var result = _functionalService.Delete<MeetingRoom>(Convert.ToInt32(payload.key));
            return Ok();

        }
    }
}