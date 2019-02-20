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
    [Route("api/BookCar")]
    public class BookCarController : Controller
    {
        private readonly IFunctional _functionalService;
        private readonly IMapper _mapper;

        public BookCarController(IFunctional functionalService,
            IMapper mapper)
        {
            _functionalService = functionalService;
            _mapper = mapper;
        }

        [HttpPost("[action]")]
        public IActionResult Get()
        {
            List<BookCar> books = _functionalService.GetList<BookCar>().ToList();
            return Ok(books);
        }

        [HttpPost("[action]")]
        public IActionResult Crud([FromBody]ScheduleViewModel<BookCarViewModel> payload)
        {
            if (payload.added.Count > 0)
            {
                BookCarViewModel value = payload.added[0];
                BookCar BookCar = new BookCar();
                value.BookCarId = 0;
                _mapper.Map<BookCarViewModel, BookCar>(value, BookCar);
                _functionalService.Insert<BookCar>(BookCar);
            }
            if (payload.changed.Count > 0)
            {
                BookCarViewModel value = payload.changed[0];
                _functionalService.Update<BookCarViewModel, BookCar>(value, Convert.ToInt32(value.BookCarId));
            }
            if (payload.deleted.Count > 0)
            {
                BookCarViewModel value = payload.deleted[0];
                var result = _functionalService.Delete<BookCar>(Convert.ToInt32(value.BookCarId));
            }

            List<BookCar> books = _functionalService.GetList<BookCar>().ToList();
            return Ok(books);
        }

        [HttpPost("[action]")]
        public IActionResult Add([FromBody]ScheduleViewModel<BookCar> payload)
        {
            if (payload.value != null)
            {
                BookCar value = payload.value;
                value.BookCarId = 0;
                _functionalService.Insert<BookCar>(value);
            }
            List<BookCar> books = _functionalService.GetList<BookCar>().ToList();
            return Ok(books);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]ScheduleViewModel<BookCarViewModel> payload)
        {
            if (payload.value != null)
            {
                BookCarViewModel value = payload.value;
                _functionalService.Update<BookCarViewModel, BookCar>(value, Convert.ToInt32(value.BookCarId));
            }
            List<BookCar> books = _functionalService.GetList<BookCar>().ToList();
            return Ok(books);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]ScheduleViewModel<BookCarViewModel> payload)
        {
            if (payload.key != null)
            {
                var result = _functionalService.Delete<BookCar>(Convert.ToInt32(payload.key));
            }
            List<BookCar> books = _functionalService.GetList<BookCar>().ToList();
            return Ok(books);
        }


    }
}