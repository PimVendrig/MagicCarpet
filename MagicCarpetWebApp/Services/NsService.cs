using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MagicCarpetWebApp.Models;
using RestSharp;

namespace MagicCarpetWebApp.Services
{
    public class NsService : INsService
    {

        protected RestClient Client { get; set; }

        public NsService()
        {
            Client = new RestClient("https://api-access.ns-mlab.nl/");
        }

        protected void DecorateRequest(RestRequest request)
        {
            request.AddHeader("x-api-key", "hackatrainapikey2018");
        }

        public StationsResult GetStations()
        {
            var request = new RestRequest("/reisinfo/api/v2/stations");
            DecorateRequest(request);
            var stations = Client.Get<StationsResult>(request);
            return stations.Data;
        }

        public PriceResult GetPrice(string fromStation, string toStation, int amount)
        {
            var request = new RestRequest("/reisinfo/api/v2/price");
            request.AddQueryParameter("fromStation", fromStation);
            request.AddQueryParameter("toStation", toStation);
            //We need the price, so just a random date in the future (for now)
            request.AddQueryParameter("plannedFromTime", "2018-09-27T09:32:00+0200");
            request.AddQueryParameter("travelClass", "2");
            request.AddQueryParameter("travelType", "single");
            request.AddQueryParameter("adults", amount.ToString());
            request.AddQueryParameter("children", "0");

            DecorateRequest(request);
            //Getting some errors here. Investigate later
            var price = Client.Get<PriceResult>(request);
            return price.Data;
        }




    }
}
