using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IPriceExtractorService
    {
        public void GetCurrentHourlyPriceAsync(object state);
    }
}
