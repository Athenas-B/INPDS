using System;
using INPDS_Core.Model;

namespace INPDS_Core.Observers
{
    public class CarNavigationNotifier : Interfaces.IObserver<Trip>
    {
        public void Update(Trip trip)
        {
            Console.WriteLine("Zasílání jízdy do navigačního systému nákladního auta. Jízda: '{0}'", trip);
        }
    }
}