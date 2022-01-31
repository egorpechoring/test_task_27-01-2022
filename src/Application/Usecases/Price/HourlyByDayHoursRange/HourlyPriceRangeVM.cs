using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Usecases.Price.HourlyByDayHoursRange
{
    public class HourlyPriceRangeVM
    {
        public string Region { get; set; }
        public List<HourlyPriceVM> HourlyPriceVMs { get; set; }
    }
    public class HourlyPriceVM
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public double Price { get; set; }
    }
}
