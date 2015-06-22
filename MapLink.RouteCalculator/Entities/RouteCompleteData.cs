using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MapLink.RouteCalculator.Entities
{
    [DataContract(Namespace="http://schemas.maplink.com.br/v1.0/RouteCalculator")]
    public class RouteCompleteData
    {
        [DataMember]
        public Route Route { get; set; }

        [DataMember]
        public string TotalTime { get; set; }

        [DataMember]
        public double KmDistance { get; set; }

        [DataMember]
        public double TotalFuelCost { get; set; }

        [DataMember]
        public double TotalCost { get; set; }
    }
}
