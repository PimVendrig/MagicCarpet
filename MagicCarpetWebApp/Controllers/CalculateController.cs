using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MagicCarpetWebApp.Models;
using MagicCarpetWebApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicCarpetWebApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Calculate")]
    public class CalculateController : Controller
    {

        private readonly MagicCarpetWebAppContext _context;
        private readonly INsService _nsService;

        public CalculateController(MagicCarpetWebAppContext context, INsService nsService)
        {
            _context = context;
            _nsService = nsService;
        }

        [Route("GetImageUrl")]
        [HttpGet]
        public string GetImageUrl(Guid concertId)
        {
            return _context.ConcertInfoes.SingleOrDefault(c => c.Id == concertId)?.Image;
        }

        [Route("GetStillNeededSeats")]
        [HttpGet]
        public int GetStillNeededSeats(Guid concertId, string destination)
        {
            //For now it is implemented simple: No aggregation of routes, one train from a concert to a destionation (Don't bundle Zwolle and Groningen for now)
            int seatsTaken = _context.Reservations
                .Where(r => r.ConcertId == concertId
                            && r.Destination == destination)
                .Sum(r => r.Amount);

            const int requiredSeats = 100;

            return seatsTaken > requiredSeats ? 0 : requiredSeats - seatsTaken;
        }

        [Route("GetPrice")]
        [HttpGet]
        public int GetPrice(Guid concertId, string destination, int amount)
        {
            string start = _context.ConcertInfoes
                .Include(c => c.Location)
                .Single(c => c.Id == concertId)
                .Location.Station;

            var price = _nsService.GetPrice(start, destination, amount);

            return price.totalPriceInCents;
        }

    }
}