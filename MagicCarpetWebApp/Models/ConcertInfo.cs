using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MagicCarpetWebApp.Models
{
    public class ConcertInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Image { get; set; }
        public Guid LocationId { get; set; }
        [ForeignKey("LocationId")]
        public ConcertLocation Location { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
