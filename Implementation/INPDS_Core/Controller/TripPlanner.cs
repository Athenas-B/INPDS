using System.Collections.Generic;
using System.Linq;
using INPDS_Core.DataAccess;
using INPDS_Core.DTO;
using INPDS_Core.Interfaces;
using INPDS_Core.Model;
using INPDS_Core.Observers;

namespace INPDS_Core.Controller
{
    public class TripPlanner : ITripPlanner
    {
        private readonly List<IObserver<Trip>> _observers = new List<IObserver<Trip>>();

        public void AddObserver(IObserver<Trip> observer)
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

        public ValidationResult PlanTrip(Order primaryOrder)
        {
            if (primaryOrder == null)
            {
                return ValidationResult.Error("Zakázka nebyla vybrána.");
            }

            using (var context = new ReturnFreightContext())
            {
                context.Orders.Attach(primaryOrder);

                // Is trip with this order already in DB?
                var validationResult = ValidateOrderNotPlannedYet(primaryOrder, context);
                if (!validationResult.IsValid)
                {
                    return validationResult;
                }

                // Add a new trip
                var newTrip = new Trip(primaryOrder);
                context.Trips.Add(newTrip);
                return TrySaveAndNotify(context, newTrip);
            }
        }


        public ValidationResult PlanTrip(Trip trip, Order secondaryOrder)
        {
            if (trip == null || secondaryOrder == null)
            {
                return ValidationResult.Error("Existující jízda a/nebo zakázka nebyla vybrána.");
            }

            if (trip.SecondaryOrder != null)
            {
                return ValidationResult.Error("Vybraná jízda již má naplánovanou objednávku pro zpáteční cestu.");
            }

            using (var context = new ReturnFreightContext())
            {
                context.Orders.Attach(secondaryOrder);
                context.Trips.Attach(trip);

                // Is trip with this order already in DB?
                var validationResult = ValidateOrderNotPlannedYet(secondaryOrder, context);
                if (!validationResult.IsValid)
                {
                    return validationResult;
                }
                trip.SecondaryOrder = secondaryOrder;
                return TrySaveAndNotify(context, trip);
            }
        }

        public void RemoveObserver(IObserver<Trip> observer)
        {
            _observers.Remove(observer);
        }

        private ValidationResult TrySaveAndNotify(ReturnFreightContext context, Trip newTrip)
        {
            var savedChanges = context.TrySaveChanges();
            if (savedChanges.IsValid)
            {
                NotifyObservers(newTrip);
            }
            return savedChanges;
        }

        private static ValidationResult ValidateOrderNotPlannedYet(Order order, ReturnFreightContext context)
        {
            var count =
                context.Trips.Count(trip => trip.PrimaryOrder.Id == order.Id || trip.SecondaryOrder.Id == order.Id);
            return count == 0
                ? ValidationResult.Ok()
                : ValidationResult.Error("Pro zadanou objednávku je již jízda naplánovaná.");
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