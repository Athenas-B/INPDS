using System;
using INPDS_Core.Model;

namespace INPDS_Core.Observers
{
    public class SMSSender : Interfaces.IObserver<Trip>
    {
        public void Update(Trip trip)
        {
            Console.WriteLine("Zasílání SMS řidiči, který jízdu uskuteční. Jízda: '{0}'", trip);
        }
    }
}