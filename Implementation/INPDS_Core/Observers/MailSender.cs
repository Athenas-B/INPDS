using System;
using INPDS_Core.Model;

namespace INPDS_Core.Observers
{
    public class MailSender : Interfaces.IObserver<Trip>
    {
        public void Update(Trip trip)
        {
            Console.WriteLine("Zasílání emailu zákazníkovi s informací o naplánované jízdě. Jízda: '{0}'", trip);
        }
    }
}