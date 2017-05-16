using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INPDS_Core.Model;

namespace INPDS_Core.Interfaces
{
    public interface IObserver
    {
        void Update(Trip trip);
    }
}
