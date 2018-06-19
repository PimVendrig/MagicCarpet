using MagicCarpetWebApp.Models;

namespace MagicCarpetWebApp.Services
{
    public interface INsService
    {
        StationsResult GetStations();
        PriceResult2 GetPrice(string fromStation, string toStation, int amount);
    }
}