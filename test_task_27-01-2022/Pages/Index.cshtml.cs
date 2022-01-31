using Application.Usecases.Price;
using Application.Usecases.Price.AverageDailyByDaysRange;
using Application.Usecases.Price.HourlyByDayHoursRange;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test_task_27_01_2022.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;

        public IndexModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        public DailyPriceRangeVM DailyPriceRangeVM { get; set; }
        
        public HourlyPriceRangeVM HourlyPriceRangeVM { get; set; }

        public static void OnGet(){}

        public async Task<IActionResult> OnPostDailyAverage()
        {
            GetDailyAveragePriceQuery getDailyAveragePriceQuery = new GetDailyAveragePriceQuery() {
                Region = Request.Form["region"], 
                StartDateTime = BuildDateTime(Request.Form["start"].ToString()), 
                EndDateTime = BuildDateTime(Request.Form["end"].ToString())
            };
            DailyPriceRangeVM = await _mediator.Send(getDailyAveragePriceQuery);

            return Page();
        }

        public async Task<IActionResult> OnPostHourly()
        {
            GetHourlyPriceQuery getHourlyPriceQuery = new GetHourlyPriceQuery()
            {
                Region = Request.Form["region"],
                StartDateTime = BuildDateTime(Request.Form["start"].ToString()),
                EndDateTime = BuildDateTime(Request.Form["end"].ToString())
            };
            HourlyPriceRangeVM = await _mediator.Send(getHourlyPriceQuery);

            return Page();
        }

        private DateTime BuildDateTime(string str)
        {
            var d = str.Split("-");
            return new DateTime(int.Parse(d[0]), int.Parse(d[1]), int.Parse(d[2]));
        }
    }
}
