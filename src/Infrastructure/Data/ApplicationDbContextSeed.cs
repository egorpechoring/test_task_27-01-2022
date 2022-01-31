using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            if (!context.HourlyPrices.Any())
            {
                Random rnd = new Random();
                int day = 3;
                int hour = 15;

                for(int i = 0; i < 48; i++)
                {
                    hour ++;

                    if(hour > 23){
                        hour = 0;
                        day++;
                    }

                    context.HourlyPrices.Add(new HourlyPrice
                    {
                        Region = "EE",
                        Year = 2022,
                        Month = 1,
                        Day = day,
                        Hour = hour,
                        Price = rnd.NextDouble() * 50 + 85
                    });
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
