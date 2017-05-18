using INPDS_Core.Interfaces;
using INPDS_Core.Model;

namespace INPDS_Core.Observers
{
    public class SMSSender : IObserver<Trip>
    {
        public void Update(Trip trip)
        {
            //TODO: Send SMS
        }
    }
}