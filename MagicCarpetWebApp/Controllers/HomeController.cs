using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MagicCarpetWebApp.Models;
using RestSharp;

namespace MagicCarpetWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult NsApi()
        {

            var client = new RestClient("https://api-access.ns-mlab.nl/");
            var restRequest = new RestRequest("/reisinfo/api/v2/stations");
            restRequest.AddHeader("x-api-key", "hackatrainapikey2018");
            var stations = client.Get<StationsResult>(restRequest);

            return View(stations.Data);
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
