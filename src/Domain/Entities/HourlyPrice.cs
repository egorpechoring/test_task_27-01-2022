using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class HourlyPrice
    {
        public int Id { get; set; }
        public string Region { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public double Price { get; set; }
    }
}
