using MagicCarpetWebApp.Models;

namespace MagicCarpetWebApp.Services
{
    public interface INsService
    {
        StationsResult GetStations();
        PriceResult GetPrice(string fromStation, string toStation, int amount);
    }
}