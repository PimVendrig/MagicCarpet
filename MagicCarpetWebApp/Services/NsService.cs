using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MagicCarpetWebApp.Models;
using Microsoft.CodeAnalysis;
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

        public PriceResult2 GetPrice(string fromStation, string toStation, int amount)
        {
            var request = new RestRequest("/reisinfo/api/v2/price");
            request.AddQueryParameter("fromStation", fromStation);
            request.AddQueryParameter("toStation", toStation);
            //We need the price, so just a random date in the future (for now)
            request.AddQueryParameter("plannedFromTime", "2018-06-20T14:28:00+02:00");
            request.AddQueryParameter("travelClass", "2");
            request.AddQueryParameter("travelType", "single");
            request.AddQueryParameter("adults", amount.ToString());
            request.AddQueryParameter("children", "0");

            DecorateRequest(request);
            var price = Client.Get<PriceResult>(request);
            if (price.IsSuccessful)
            {
                return price.Data.payload;
            }
            //Getting some errors here. Investigate later
            return new PriceResult2 { totalPriceInCents = 955 * amount };//Fake ticketprice to Euro 9,55
        }

        public bool BuyDemo()
        {
            var request = new RestRequest("tickets/api/v1/buy", Method.POST);

            request.AddParameter("application/json", "{\r\n\"bookerEmail\": \"sjoerd.smink@ns.nl\",\r\n\"serviceNewsletter\": false,\r\n\"offerNewsletter\": true,\r\n\"surveyNewsletter\": false,\r\n\"bank\": \"INGBNL2A\",\r\n\"expectedPrice\": 725,\r\n\"uuid\": \"1234\",\r\n\"products\": [{\r\n\"productName\": \"enkele-reis\",\r\n\"travelDate\": \"2018-06-20\",\r\n\"fromStation\": \"ed\",\r\n\"toStation\": \"amf\",\r\n\"route\": 92,\r\n\"travelClass\": 2,\r\n\"travelers\": [{\"initials\": \"SK\", \"lastName\": \"Smink\", \"email\": \"sjoerd.smink@ns.nl\", \"birthDate\": \"1988-10-30\" }]\r\n}]\r\n}", ParameterType.RequestBody);
            DecorateRequest(request);

            var response = Client.Post(request);
            return response.IsSuccessful;
        }


    }
}
