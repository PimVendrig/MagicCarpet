using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MagicCarpetWebApp.Services
{
    [TestClass]
    public class NsServiceTest
    {
        [TestMethod]
        public void BuyDemoTest()
        {
            //arrange
            var service = new NsService();
            //act
            var result = service.BuyDemo();
            //assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetPriceTest()
        {
            //arrange
            var service = new NsService();
            //act
            var result = service.GetPrice("ASB", "HGV", 1);
            //assert
            Assert.AreNotEqual(955, result.totalPriceInCents); //955 is used as a fake price. Assert it is not that price
        }

    }
}
