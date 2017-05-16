using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INPDS_Core.DataAccess;
using INPDS_Core.DTO;
using INPDS_Core.Interfaces;
using INPDS_Core.Model;
using INPDS_Core.Observers;

namespace INPDS_Core.Controller
{
    public class TripPlanner : ITripPlanner
    {
        private List<IObserver> _observers = new List<IObserver>();

        public void AddObserver(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void NotifyObservers(Trip trip)
        {
            foreach (var observer in _observers)
            {
                observer.Update(trip);
            }
        }

        public ValidationResult PlanTrip(Order primaryOrder, Order secondaryOrder = null)
        {
            using (var context = new ReturnFreightContext())
            {
                // Is trip with this primary order already in DB?
                var foundTrip = context.Trips.FirstOrDefault(trip => trip.PrimaryOrder == primaryOrder);

                context.Orders.Attach(primaryOrder);
                context.Orders.Attach(secondaryOrder);
                if (foundTrip == null)
                {
                    // Add a new trip
                    Trip newTrip = new Trip(primaryOrder, secondaryOrder);
                    context.Trips.Add(newTrip);
                    return context.TrySaveChanges();
                }
                else
                {
                    // Update existing trip
                    context.Trips.Attach(foundTrip);
                    foundTrip.SecondaryOrder = secondaryOrder;
                    return context.TrySaveChanges();
                }
            }
        }

        public void RemoveObserver(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public static ITripPlanner CreateTripPlanner()
        {
            ITripPlanner planner = new TripPlanner();
            planner.AddObserver(new SMSSender());
            planner.AddObserver(new MailSender());
            planner.AddObserver(new CarNavigationNotifier());
            planner.AddObserver(new EnviromentDepartmentNotifier());
            return planner;
        }
    }
}
