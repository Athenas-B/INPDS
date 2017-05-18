using INPDS_Core.DTO;
using INPDS_Core.Interfaces;
using INPDS_Core.Model;

namespace INPDS_Core.Controller
{
    public interface ITripPlanner : IObservable<Trip>
    {
        ValidationResult PlanTrip(Order primaryOrder);
        ValidationResult PlanTrip(Trip trip, Order secondaryOrder);
    }
}