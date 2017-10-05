using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckServices.Model.Exceptions
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException()
        {

        }

        public AuthenticationException(string message)
            : base(message)
        {

        }
    }
}
