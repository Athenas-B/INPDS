using INPDS_Core.Model;

namespace INPDS_Core.Controller
{
    public interface IOrderController
    {
        /// <param name="order"></param>
        void RegisterOrder(Order order);
    }
}