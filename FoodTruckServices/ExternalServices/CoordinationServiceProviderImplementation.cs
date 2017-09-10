using FoodTruckServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodTruckServices.Model;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using FoodTruckServices.ExternalServices.Responses;

namespace FoodTruckServices.ExternalServices
{
    public class CoordinationServiceProviderImplementation : ICoordinationServiceProvider
    {
        public CoordinationServiceProviderImplementation()
        {

        }

        public async Task<Coordination> GetLatAndLongByAddress(Address address)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var inputParameter = string.Format(Constants.GoogleMapApiUrl, address.ToAddressString());
            var responseString = client.GetStringAsync(inputParameter);
            var finalResponseString = await responseString;
            var googleMapAddresssResponse = JsonConvert.DeserializeObject<GoogleMapAddressResponse>(finalResponseString);

            return new Coordination()
            {
                Latitude = Convert.ToDecimal(googleMapAddresssResponse.results[0].geometry.location.lat),
                Longitude = Convert.ToDecimal(googleMapAddresssResponse.results[0].geometry.location.lng)
            };
        }
    }
}
