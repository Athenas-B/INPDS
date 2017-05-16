using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INPDS_Core.Interfaces;
using INPDS_Core.Model;

namespace INPDS_Core.Observers
{
    public class MailSender : IObserver
    {
        public void Update(Trip trip)
        {
            //TODO: Send Mail
        }
    }
}
