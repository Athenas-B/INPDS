using INPDS_Core.DataAccess;
using INPDS_Core.DTO;
using INPDS_Core.Model;

namespace INPDS_Core.Controller
{
    public class InvoiceController : IInvoiceController
    {
        public ValidationResult RegisterInvoice(Invoice invoice)
        {
            var validationResult = Validate(invoice);
            if (!validationResult.IsValid) return validationResult;
            using (var context = new ReturnFreightContext())
            {
                context.Orders.Attach(invoice.Order);
                context.Invoices.Add(invoice);
                return context.TrySaveChanges();
            }
        }

        private static ValidationResult Validate(Invoice invoice)
        {
            var result = ValidationResult.Ok();
            if (invoice.Price < 0)
            {
                result = result.JoinResults(ValidationResult.Error("Částka nebyla zadána."));
            }
            return result;
        }
    }
}