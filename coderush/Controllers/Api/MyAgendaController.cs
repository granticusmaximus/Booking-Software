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
using Microsoft.AspNetCore.Identity;

namespace coderush.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/MyAgenda")]
    public class MyAgendaController : Controller
    {
        private readonly IFunctional _functionalService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public MyAgendaController(IFunctional functionalService,
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            _functionalService = functionalService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Get()
        {
            ApplicationUser appUser = await _userManager.GetUserAsync(User);
            List<MyAgenda> books = new List<MyAgenda>();
            if (appUser != null)
            {
                books = _functionalService.GetList<MyAgenda>()
                    .Where(x => x.CreateBy.Equals(appUser.Id)).ToList();
            }
            return Ok(books);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Crud([FromBody]ScheduleViewModel<MyAgendaViewModel> payload)
        {
            if (payload.added.Count > 0)
            {
                MyAgendaViewModel value = payload.added[0];
                MyAgenda MyAgenda = new MyAgenda();
                value.MyAgendaId = 0;
                _mapper.Map<MyAgendaViewModel, MyAgenda>(value, MyAgenda);
                _functionalService.Insert<MyAgenda>(MyAgenda);
            }
            if (payload.changed.Count > 0)
            {
                MyAgendaViewModel value = payload.changed[0];
                _functionalService.Update<MyAgendaViewModel, MyAgenda>(value, Convert.ToInt32(value.MyAgendaId));
            }
            if (payload.deleted.Count > 0)
            {
                MyAgendaViewModel value = payload.deleted[0];
                var result = _functionalService.Delete<MyAgenda>(Convert.ToInt32(value.MyAgendaId));
            }

            ApplicationUser appUser = await _userManager.GetUserAsync(User);
            List<MyAgenda> books = new List<MyAgenda>();
            if (appUser != null)
            {
                books = _functionalService.GetList<MyAgenda>()
                    .Where(x => x.CreateBy.Equals(appUser.Id)).ToList();
            }
            return Ok(books);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add([FromBody]ScheduleViewModel<MyAgenda> payload)
        {
            if (payload.value != null)
            {
                MyAgenda value = payload.value;
                value.MyAgendaId = 0;
                _functionalService.Insert<MyAgenda>(value);
            }
            ApplicationUser appUser = await _userManager.GetUserAsync(User);
            List<MyAgenda> books = new List<MyAgenda>();
            if (appUser != null)
            {
                books = _functionalService.GetList<MyAgenda>()
                    .Where(x => x.CreateBy.Equals(appUser.Id)).ToList();
            }
            return Ok(books);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Update([FromBody]ScheduleViewModel<MyAgendaViewModel> payload)
        {
            if (payload.value != null)
            {
                MyAgendaViewModel value = payload.value;
                _functionalService.Update<MyAgendaViewModel, MyAgenda>(value, Convert.ToInt32(value.MyAgendaId));
            }
            ApplicationUser appUser = await _userManager.GetUserAsync(User);
            List<MyAgenda> books = new List<MyAgenda>();
            if (appUser != null)
            {
                books = _functionalService.GetList<MyAgenda>()
                    .Where(x => x.CreateBy.Equals(appUser.Id)).ToList();
            }
            return Ok(books);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Remove([FromBody]ScheduleViewModel<MyAgendaViewModel> payload)
        {
            if (payload.key != null)
            {
                var result = _functionalService.Delete<MyAgenda>(Convert.ToInt32(payload.key));
            }
            ApplicationUser appUser = await _userManager.GetUserAsync(User);
            List<MyAgenda> books = new List<MyAgenda>();
            if (appUser != null)
            {
                books = _functionalService.GetList<MyAgenda>()
                    .Where(x => x.CreateBy.Equals(appUser.Id)).ToList();
            }
            return Ok(books);
        }


    }
}