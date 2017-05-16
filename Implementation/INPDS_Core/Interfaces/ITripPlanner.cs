using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INPDS_Core.DTO;
using INPDS_Core.Interfaces;
using INPDS_Core.Model;

namespace INPDS_Core.Controller
{
    public interface ITripPlanner : IObservable
    {
        ValidationResult PlanTrip(Order primaryOrder, Order secondaryOrder = null);
    }
}
