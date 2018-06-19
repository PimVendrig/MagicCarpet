using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagicCarpetWebApp.Models
{
    public class ConcertLocation
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Station { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
