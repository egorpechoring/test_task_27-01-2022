using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Usecases.Price.AverageDailyByDaysRange
{
    public class DailyPriceRangeVM
    {
        public string Region { get; set; }
        public List<DailyPriceVM> DailyPriceVMs { get; set; }
    }
    public class DailyPriceVM
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public double AveragePrice { get; set; }
    }
}
