using INPDS_Core.Interfaces;
using INPDS_Core.Model;

namespace INPDS_Core.Observers
{
    public class MailSender : IObserver<Trip>
    {
        public void Update(Trip trip)
        {
            //TODO: Send Mail
        }
    }
}