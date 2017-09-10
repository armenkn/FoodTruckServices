using FoodTruckServices.Model;
using System.Threading.Tasks;

namespace FoodTruckServices.Interfaces
{
    public interface ICoordinationServiceProvider
    {
       Task<Coordination> GetLatAndLongByAddress(Address address);
    }
}
