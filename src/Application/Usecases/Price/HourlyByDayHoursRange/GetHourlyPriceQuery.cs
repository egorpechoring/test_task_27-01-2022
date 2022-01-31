using Application.Common.Interfaces;
using Application.Usecases.Price.HourlyByDayHoursRange;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Usecases.Price
{
    public class GetHourlyPriceQuery : IRequest<HourlyPriceRangeVM>
    {
        public string Region { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }

    public class GetHourlyPriceQueryHandler : IRequestHandler<GetHourlyPriceQuery, HourlyPriceRangeVM>
    {
        private readonly IApplicationDbContext _context;

        public GetHourlyPriceQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<HourlyPriceRangeVM> Handle(GetHourlyPriceQuery request, CancellationToken cancellationToken)
        {
            HourlyPriceRangeVM res = new HourlyPriceRangeVM { Region = request.Region, HourlyPriceVMs = new List<HourlyPriceVM>() };

            try
            {
                _context.HourlyPrices
                    .Where(elem => elem.Region == request.Region)
                    .Where(elem => elem.Year >= request.StartDateTime.Year && elem.Year <= request.EndDateTime.Year)
                    .Where(elem => elem.Month >= request.StartDateTime.Month && elem.Month <= request.EndDateTime.Month)
                    .Where(elem => elem.Day >= request.StartDateTime.Day && elem.Day <= request.EndDateTime.Day)
                    .OrderBy(elem => elem.Year)
                    .ThenBy(elem => elem.Month)
                    .ThenBy(elem => elem.Day)
                    .ThenBy(elem => elem.Hour)
                    .ToList()
                    .ForEach(elem => res.HourlyPriceVMs.Add(new HourlyPriceVM{
                        Year = elem.Year,
                        Month = elem.Month,
                        Day = elem.Day,
                        Hour = elem.Hour,
                        Price = elem.Price
                    }));
            } catch (Exception e)
            {
                //log exception 
            }

            return res;
        }
    }
}
