using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coderush.Models;
using coderush.Models.SyncfusionViewModels;
using coderush.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace coderush.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Dashboard")]
    public class DashboardController : Controller
    {
        private readonly IFunctional _functionalService;

        public DashboardController(IFunctional functionalService)
        {
            _functionalService = functionalService;
        }
        

        [HttpGet("[action]")]
        public IActionResult BookingTrends()
        {
            List<ChartViewModel> meetingRoom = new List<ChartViewModel>();
            List<ChartViewModel> car = new List<ChartViewModel>();
            List<ChartViewModel> general = new List<ChartViewModel>();
            DateTime start = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime end = start.Date.AddMonths(11);
            for (DateTime i = start.Date; i <= end.Date; i = i.Date.AddMonths(1))
            {
                double countMeetingRoom = _functionalService.GetList<BookMeetingRoom>()
                    .Where(x => x.StartTime.Month.Equals(i.Month) && x.StartTime.Year.Equals(i.Year)).Count();
                meetingRoom.Add(new ChartViewModel { x = i.Date.ToString("MMM"), y = countMeetingRoom });

                double countCar = _functionalService.GetList<BookCar>()
                    .Where(x => x.StartTime.Month.Equals(i.Month) && x.StartTime.Year.Equals(i.Year)).Count();
                car.Add(new ChartViewModel { x = i.Date.ToString("MMM"), y = countCar });

                double countGeneral = _functionalService.GetList<BookGeneral>()
                    .Where(x => x.StartTime.Month.Equals(i.Month) && x.StartTime.Year.Equals(i.Year)).Count();
                general.Add(new ChartViewModel { x = i.Date.ToString("MMM"), y = countGeneral });
                
            }
            return Ok(new
            {
                dataMeetingRoom = meetingRoom,
                textMeetingRoom = "Meeting Room",
                dataCar = car,
                textCar = "Car",
                dataGeneral = general,
                textGeneral = "General"
            });
        }
    }
}