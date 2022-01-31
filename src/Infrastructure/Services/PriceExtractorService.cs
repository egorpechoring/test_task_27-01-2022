using Application.Common.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class PriceExtractorService : IPriceExtractorService, IHostedService, IDisposable
    {
        private readonly ILogger<PriceExtractorService> _logger;
        private readonly IApplicationDbContext _context;
        private Timer _timer = null!;

        public PriceExtractorService(ILogger<PriceExtractorService> logger, IApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");
            _timer = new Timer(GetCurrentHourlyPriceAsync, null, 0, 60 * 60 * 1000);
            return Task.CompletedTask;
        }

        public async void GetCurrentHourlyPriceAsync(object state)
        {
            bool IsResponceBody = false;
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("https://dashboard.elering.ee/api/nps/price/EE/current");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                IsResponceBody = true;
            }
            catch (HttpRequestException e)
            {
                //log exeption
            }
            if(IsResponceBody)
            {
                //save to db
                //_context.HourlyPrices.Add(new HourlyPrice { .... });
                //await _context.SaveChangesAsync(new CancellationToken());
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

    }
}
