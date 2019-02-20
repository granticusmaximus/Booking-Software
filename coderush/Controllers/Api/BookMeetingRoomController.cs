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
using coderush.Services;
using AutoMapper;

namespace coderush.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/BookMeetingRoom")]
    public class BookMeetingRoomController : Controller
    {
        private readonly IFunctional _functionalService;
        private readonly IMapper _mapper;

        public BookMeetingRoomController(IFunctional functionalService,
            IMapper mapper)
        {
            _functionalService = functionalService;
            _mapper = mapper;
        }

        [HttpPost("[action]")]
        public IActionResult Get()
        {
            List<BookMeetingRoom> books = _functionalService.GetList<BookMeetingRoom>().ToList();
            return Ok(books);
        }

        [HttpPost("[action]")]
        public IActionResult Crud([FromBody]ScheduleViewModel<BookMeetingRoomViewModel> payload)
        {
            if (payload.added.Count > 0)
            {
                BookMeetingRoomViewModel value = payload.added[0];
                BookMeetingRoom BookMeetingRoom = new BookMeetingRoom();
                value.BookMeetingRoomId = 0;
                _mapper.Map<BookMeetingRoomViewModel, BookMeetingRoom>(value, BookMeetingRoom);
                _functionalService.Insert<BookMeetingRoom>(BookMeetingRoom);
            }
            if (payload.changed.Count > 0)
            {
                BookMeetingRoomViewModel value = payload.changed[0];
                _functionalService.Update<BookMeetingRoomViewModel, BookMeetingRoom>(value, Convert.ToInt32(value.BookMeetingRoomId));
            }
            if (payload.deleted.Count > 0)
            {
                BookMeetingRoomViewModel value = payload.deleted[0];
                var result = _functionalService.Delete<BookMeetingRoom>(Convert.ToInt32(value.BookMeetingRoomId));
            }

            List<BookMeetingRoom> books = _functionalService.GetList<BookMeetingRoom>().ToList();
            return Ok(books);
        }

        [HttpPost("[action]")]
        public IActionResult Add([FromBody]ScheduleViewModel<BookMeetingRoom> payload)
        {
            if (payload.value != null)
            {
                BookMeetingRoom value = payload.value;
                value.BookMeetingRoomId = 0;
                _functionalService.Insert<BookMeetingRoom>(value);
            }
            List<BookMeetingRoom> books = _functionalService.GetList<BookMeetingRoom>().ToList();
            return Ok(books);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]ScheduleViewModel<BookMeetingRoomViewModel> payload)
        {
            if (payload.value != null)
            {
                BookMeetingRoomViewModel value = payload.value;
                _functionalService.Update<BookMeetingRoomViewModel, BookMeetingRoom>(value, Convert.ToInt32(value.BookMeetingRoomId));
            }
            List<BookMeetingRoom> books = _functionalService.GetList<BookMeetingRoom>().ToList();
            return Ok(books);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]ScheduleViewModel<BookMeetingRoomViewModel> payload)
        {
            if (payload.key != null)
            {
                var result = _functionalService.Delete<BookMeetingRoom>(Convert.ToInt32(payload.key));
            }
            List<BookMeetingRoom> books = _functionalService.GetList<BookMeetingRoom>().ToList();
            return Ok(books);
        }


    }
}