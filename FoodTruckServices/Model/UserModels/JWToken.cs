using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckServices.Model
{
    public class JWToken
    {
        public Header Header { get; set; }

        public Payload Payload { get; set; }

        public string VerifySignature { get; set; }
    }

    public class Header
    {
        public string alg { get; set; }

        public string typ { get; set; }
    }

    public class Payload
    {        
        public int UserId { get; set; }

        public int UserRoleId { get; set; }

        public DateTime CreateDateTime { get; set; }

        public int LifespanMins { get; set; }
    }
    
}
