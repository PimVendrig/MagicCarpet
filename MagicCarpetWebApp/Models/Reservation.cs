using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MagicCarpetWebApp.Models
{
    public class Reservation
    {

        public Guid Id { get; set; }
        public Guid ConcertId { get; set; }
        [ForeignKey("ConcertId")] public ConcertInfo Concert { get; set; }
        public string Destination { get; set; }
        public int Amount { get; set; }
        public string EmailAddress { get; set; }
        public bool Agrees { get; set; }
        public string PaymentDetails { get; set; }
    }
}
