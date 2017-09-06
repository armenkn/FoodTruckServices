using FoodTruckServices.Model;

namespace FoodTruckServices.Interfaces
{
    public interface ICoordinationServiceProvider
    {
        Coordination GetLatAndLongByAddress(Address address);
    }
}
