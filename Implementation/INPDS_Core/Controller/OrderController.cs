using System;
using INPDS_Core.DataAccess;
using INPDS_Core.DTO;
using INPDS_Core.Model;

namespace INPDS_Core.Controller
{
    public class OrderController : IOrderController
    {
        public ValidationResult RegisterOrder(Order order)
        {
            var validationResult = Validate(order);
            if (validationResult.IsValid)
            {
                using (var context = new ReturnFreightContext())
                {
                    context.Orders.Add(order);
                    context.SaveChanges();
                }
            }
            return validationResult;
        }

        private static ValidationResult Validate(Order order)
        {
            ValidationResult result = ValidationResult.Ok();
            if (order.Customer == null)
            {
                result = result.JoinResults(ValidationResult.Error("U�ivatel nen� definov�n."));
            }
            if (DateTime.Now > order.DeliveryDeadline)
            {
                result = result.JoinResults(ValidationResult.Error("Term�n nejpozd�j��ho dod�n� mus� b�t v budoucnosti."));
            }
            if (DateTime.Now > order.PickupDate)
            {
                result = result.JoinResults(ValidationResult.Error("Term�n vyzvednut� zbo�� mus� b�t v budoucnosti."));
            }
            if (order.PickupDate > order.DeliveryDeadline)
            {
                result = result.JoinResults(ValidationResult.Error("Term�n nejpozd�j��ho dod�n� mus� b�t v�t�� ne� term�n vyzvednut�."));
            }
            if (string.IsNullOrWhiteSpace(order.From) || string.IsNullOrWhiteSpace(order.To))
            {
                result = result.JoinResults(ValidationResult.Error("Beginning or destination of the order is invalid."));
            }
            return result;
        }
    }
}