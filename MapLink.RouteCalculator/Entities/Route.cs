using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MapLink.RouteCalculator.Entities
{
    [DataContract(Namespace = "http://schemas.maplink.com.br/v1.0/RouteCalculator")]
    public class Route
    {
        [DataMember]
        public AddressData Origin { get; set; }

        [DataMember]
        public AddressData Destination { get; set; }

        [DataMember]
        public RouteType RouteType { get; set; }
    }
}
