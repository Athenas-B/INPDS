using INPDS_Core.Interfaces;
using INPDS_Core.Model;

namespace INPDS_Core.Observers
{
    public class EnviromentDepartmentNotifier : IObserver<Trip>
    {
        public void Update(Trip trip)
        {
            //TODO: Notify Enviroment Department
        }
    }
}