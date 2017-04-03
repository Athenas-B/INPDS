using INPDS_Core.DTO;
using INPDS_Core.Model;

namespace INPDS_Core.Controller
{
    public interface IOrderController
    {
        /// <param name="order"></param>
        ValidationResult RegisterOrder(Order order);
    }
}