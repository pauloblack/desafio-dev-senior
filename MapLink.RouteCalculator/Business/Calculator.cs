using MapLink.RouteCalculator.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapLink.RouteCalculator.Business
{
    public class Calculator
    {
        public Calculator(string token)
        {
            this.token = token;
        }

        public RouteCompleteData[] CalculateRoute(Route[] routeList)
        {
            RouteCompleteData[] calculatedRoutes = new RouteCompleteData[routeList.Length];

            for (int i = 0; i < routeList.Length; i++)
            {
                Route routeItem = routeList[i];

                if (routeItem == null)
                    throw new Exception("Route cannot be null");
                else if(routeItem.Origin == null)
                    throw new Exception("Origin cannot be null");
                else if (routeItem.Destination == null)
                    throw new Exception("Destination cannot be null");

                ////////////////////////////////////////////////////////////////////////////////////
                // Completar os objetos com coordenadas
                AddressFinderService.Point originCoordinates = GetCoordinates(routeItem.Origin.Address,
                    routeItem.Origin.Number, routeItem.Origin.City, routeItem.Origin.State);

                routeItem.Origin.CoordinateX = originCoordinates.x;
                routeItem.Origin.CoordinateY = originCoordinates.y;

                AddressFinderService.Point destinationCoordinates = GetCoordinates(routeItem.Destination.Address,
                    routeItem.Destination.Number, routeItem.Destination.City, routeItem.Destination.State);

                routeItem.Destination.CoordinateX = destinationCoordinates.x;
                routeItem.Destination.CoordinateY = destinationCoordinates.y;

                calculatedRoutes[i] = GetRouteData(routeItem);
            }

            return calculatedRoutes;
        }

        private string token;

        public AddressFinderService.Point GetCoordinates(string address, string number, string city, string state)
        {
            using (AddressFinderService.AddressFinderSoapClient client = new AddressFinderService.AddressFinderSoapClient())
            {
                AddressFinderService.Address addressReq = new AddressFinderService.Address();
                addressReq.city = new AddressFinderService.City();
                addressReq.city.name = city;
                addressReq.city.state = state;

                addressReq.houseNumber = number;
                addressReq.street = address;

                return client.getXY(addressReq, token);
            }
        }

        public RouteCompleteData GetRouteData(Route routeData)
        {
            RouteProximityService.RouteTotals resp = null;

            using (RouteProximityService.RouteProximitySoapClient client = new RouteProximityService.RouteProximitySoapClient())
            {
                ////////////////////////////////////////////////////////////////////////////////////////////////
                // Origem e desino
                RouteProximityService.RouteStop originRoute = new RouteProximityService.RouteStop();
                originRoute.description = string.Format("{0}, {1} - {2}/{3}", routeData.Origin.Address, routeData.Origin.Number,
                    routeData.Origin.City, routeData.Origin.State);

                originRoute.point = new RouteProximityService.Point();
                originRoute.point.x = routeData.Origin.CoordinateX;
                originRoute.point.y = routeData.Origin.CoordinateY;

                RouteProximityService.RouteStop destinationRoute = new RouteProximityService.RouteStop();
                destinationRoute.description = string.Format("{0}, {1} - {2}/{3}", routeData.Destination.Address, routeData.Destination.Number,
                    routeData.Destination.City, routeData.Destination.State);
                destinationRoute.point = new RouteProximityService.Point();
                destinationRoute.point.x = routeData.Destination.CoordinateX;
                destinationRoute.point.y = routeData.Destination.CoordinateY;

                ////////////////////////////////////////////////////////////////////////////////////////////////
                // Filtros
                RouteProximityService.RouteProximityOptions filters = new RouteProximityService.RouteProximityOptions();
                filters.language = "portugues";
                filters.routeDetails = new RouteProximityService.RouteDetails();
                filters.routeDetails.descriptionType = routeData.RouteType == RouteType.Short ? 1 : 23;
                filters.routeDetails.optimizeRoute = true;

                ////////////////////////////////////////////////////////////////////////////////////////////////////
                // informações de veículo com valores fixos pois não fazem parte dos parametros recebidos
                filters.vehicle = new RouteProximityService.Vehicle();
                filters.vehicle.tankCapacity = 45;
                filters.vehicle.averageConsumption = 9;
                filters.vehicle.fuelPrice = 3;
                filters.vehicle.averageSpeed = 90;
                filters.vehicle.tollFeeCat = 2;

                resp = client.getRouteProximityTotals(new RouteProximityService.RouteStop[] { originRoute, destinationRoute }, filters, this.token);
            }

            RouteCompleteData completeData = new RouteCompleteData();
            completeData.Route = routeData;
            completeData.KmDistance = resp.totalDistance;
            completeData.TotalCost = resp.totalCost;
            completeData.TotalTime = resp.totalTime;
            completeData.TotalFuelCost = resp.totalfuelCost;

            return completeData;
        }
    }
}
