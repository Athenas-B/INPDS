using System;
using INPDS_Core.Model;

namespace INPDS_Core.Observers
{
    public class EnviromentDepartmentNotifier : Interfaces.IObserver<Trip>
    {
        public void Update(Trip trip)
        {
            Console.WriteLine("Zasílání jízdy do statistického systému Ministerstva životního prostredí pro sledování emisí. Jízda: '{0}'", trip);
        }
    }
}