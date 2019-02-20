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
    [Route("api/BookGeneral")]
    public class BookGeneralController : Controller
    {
        private readonly IFunctional _functionalService;
        private readonly IMapper _mapper;

        public BookGeneralController(IFunctional functionalService,
            IMapper mapper)
        {
            _functionalService = functionalService;
            _mapper = mapper;
        }

        [HttpPost("[action]")]
        public IActionResult Get()
        {
            List<BookGeneral> books = _functionalService.GetList<BookGeneral>().ToList();
            return Ok(books);
        }

        [HttpPost("[action]")]
        public IActionResult Crud([FromBody]ScheduleViewModel<BookGeneralViewModel> payload)
        {
            if (payload.added.Count > 0)
            {
                BookGeneralViewModel value = payload.added[0];
                BookGeneral bookGeneral = new BookGeneral();
                value.BookGeneralId = 0;
                _mapper.Map<BookGeneralViewModel, BookGeneral>(value, bookGeneral);
                _functionalService.Insert<BookGeneral>(bookGeneral);
            }
            if (payload.changed.Count > 0)
            {
                BookGeneralViewModel value = payload.changed[0];
                _functionalService.Update<BookGeneralViewModel, BookGeneral>(value, Convert.ToInt32(value.BookGeneralId));
            }
            if (payload.deleted.Count > 0)
            {
                BookGeneralViewModel value = payload.deleted[0];
                var result = _functionalService.Delete<BookGeneral>(Convert.ToInt32(value.BookGeneralId));
            }

            List<BookGeneral> books = _functionalService.GetList<BookGeneral>().ToList();
            return Ok(books);
        }

        [HttpPost("[action]")]
        public IActionResult Add([FromBody]ScheduleViewModel<BookGeneral> payload)
        {
            if (payload.value != null)
            {
                BookGeneral value = payload.value;
                value.BookGeneralId = 0;
                _functionalService.Insert<BookGeneral>(value);
            }
            List<BookGeneral> books = _functionalService.GetList<BookGeneral>().ToList();
            return Ok(books);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]ScheduleViewModel<BookGeneralViewModel> payload)
        {
            if (payload.value != null)
            {
                BookGeneralViewModel value = payload.value;
                _functionalService.Update<BookGeneralViewModel, BookGeneral>(value, Convert.ToInt32(value.BookGeneralId));
            }
            List<BookGeneral> books = _functionalService.GetList<BookGeneral>().ToList();
            return Ok(books);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]ScheduleViewModel<BookGeneralViewModel> payload)
        {
            if (payload.key != null)
            {
                var result = _functionalService.Delete<BookGeneral>(Convert.ToInt32(payload.key));
            }
            List<BookGeneral> books = _functionalService.GetList<BookGeneral>().ToList();
            return Ok(books);
        }


    }
}