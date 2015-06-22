using MapLink.RouteCalculator.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MapLink.RouteCalculator.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(Namespace="http://schemas.maplink.com.br/v1.0/RouteCalculator")]
    public interface ICalculator
    {
        [OperationContract]
        RouteCompleteData[] CalculateRouteDetails(Route[] routeList);
    }
}
