using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MapLink.RouteCalculator.Entities
{
    [DataContract(Namespace = "http://schemas.maplink.com.br/v1.0/RouteCalculator")]
    public class AddressData
    {
        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string Number { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string State { get; set; }

        public double CoordinateX { get; set; }

        public double CoordinateY { get; set; }
    }
}
