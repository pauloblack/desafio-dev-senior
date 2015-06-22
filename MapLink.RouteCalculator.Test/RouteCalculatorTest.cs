using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MapLink.RouteCalculator.Business;
using System.Configuration;
using MapLink.RouteCalculator.Entities;

namespace MapLink.RouteCalculator.Test
{
    [TestClass]
    public class RouteCalculatorTest
    {
        [TestMethod]
        public void GetCoordinatesTest()
        {
            Calculator calc = new Calculator(ConfigurationManager.AppSettings["ServiceToken"]);
            AddressFinderService.Point result = calc.GetCoordinates("Rua Renato da Costa Bonfim", "290", "Sâo Paulo", "SP");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetRouteDataTest()
        {
            AddressData origin = new AddressData();
            origin.Address = "Rua Salvarana";
            origin.Number = "14B";
            origin.City = "Sâo Paulo";
            origin.State = "SP";
            origin.CoordinateX = -46.5074637;
            origin.CoordinateY = -23.557557;

            AddressData destination = new AddressData();
            destination.Address = "Rua Renato da Costa Bonfim";
            destination.Number = "290";
            destination.City = "Sâo Paulo";
            destination.State = "SP";
            destination.CoordinateX = -46.478805;
            destination.CoordinateY = -23.504837;

            Route route = new Route();
            route.Origin = origin;
            route.Destination = destination;
            route.RouteType = RouteType.Short;

            Calculator calc = new Calculator(ConfigurationManager.AppSettings["ServiceToken"]);
            RouteCompleteData routeData = calc.GetRouteData(route);

            Assert.IsNotNull(routeData);
        }
    }
}
