using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MapLink.RouteCalculator.Entities
{
    [DataContract(Namespace = "http://schemas.maplink.com.br/v1.0/RouteCalculator")]
    public enum RouteType
    {
        /// <summary>
        /// Rota mais tápida
        /// </summary>
        [EnumMember]
        Short,

        /// <summary>
        /// Evitar o tráfego
        /// </summary>
        [EnumMember]
        TrafficAvoid,
    }
}
