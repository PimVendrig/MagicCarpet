using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagicCarpetWebApp.Models
{
    public class PriceResult
    {

        public PriceResult2 payload { get; set; }
    }

    public class PriceResult2
    {

        public int totalPriceInCents { get; set; }
    }

}
