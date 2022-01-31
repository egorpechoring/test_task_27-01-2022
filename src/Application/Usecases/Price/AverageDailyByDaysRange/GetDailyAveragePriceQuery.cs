using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Application.Usecases.Price.AverageDailyByDaysRange;
using Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Application.Usecases.Price
{
    
    public class GetDailyAveragePriceQuery : IRequest<DailyPriceRangeVM>
    {
        public string Region { get; set; }
        public DateTime StartDateTime {get;set; }
        public DateTime EndDateTime { get; set; }
    }

    public class GetDailyAveragePriceQueryHandler : IRequestHandler<GetDailyAveragePriceQuery, DailyPriceRangeVM>
    {
        private readonly IApplicationDbContext _context;

        public GetDailyAveragePriceQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DailyPriceRangeVM> Handle(GetDailyAveragePriceQuery request, CancellationToken cancellationToken)
        {
            List<DailyPriceVM> dailyPriceVMs = new List<DailyPriceVM>();
            DateTime temp = request.StartDateTime;
            int rangeDays = request.EndDateTime.Subtract(request.StartDateTime).Days;

            for (int i = 0; i < rangeDays + 1; i++)
            {
                try
                {
                   double av = _context.HourlyPrices
                    .Where(elem => elem.Region == request.Region)
                    .Where(elem => elem.Year == temp.Year && elem.Month == temp.Month && elem.Day == temp.Day)
                    .Select(elem => elem.Price)
                    .ToList()
                    .DefaultIfEmpty(0)
                    .Average();

                    dailyPriceVMs.Add(new DailyPriceVM { Year = temp.Year, Month = temp.Month, Day = temp.Day, AveragePrice = av });
                }
                catch (Exception e)
                {
                    //log exception 
                }
                temp = temp.AddDays(1);
            }

            dailyPriceVMs
                .OrderBy(elem => elem.Year)
                .ThenBy(elem => elem.Month)
                .ThenBy(elem => elem.Day);

            return new DailyPriceRangeVM { Region = request.Region, DailyPriceVMs = dailyPriceVMs };
        }
    }
    
}
