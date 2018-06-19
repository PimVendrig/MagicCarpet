using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MagicCarpetWebApp.Models;
using RestSharp;

namespace MagicCarpetWebApp.Services
{
    public class NsService
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

    }
}
