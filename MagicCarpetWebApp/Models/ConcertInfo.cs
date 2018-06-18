using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagicCarpetWebApp.Models
{
    public class ConcertInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
