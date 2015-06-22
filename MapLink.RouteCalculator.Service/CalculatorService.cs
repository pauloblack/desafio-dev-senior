using MapLink.RouteCalculator.Business;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MapLink.RouteCalculator.Service
{
    [ServiceBehavior(Namespace = "http://schemas.maplink.com.br/v1.0/RouteCalculator")]
    public class CalculatorService 
        : ICalculator
    {

        public Entities.RouteCompleteData[] CalculateRouteDetails(Entities.Route[] routeList)
        {
            Calculator calculator = new Calculator(ConfigurationManager.AppSettings["ServiceToken"]);
            return calculator.CalculateRoute(routeList);
        }
    }
}
