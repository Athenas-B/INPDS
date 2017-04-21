using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INPDS_Core.DataAccess;
using INPDS_Core.DTO;
using INPDS_Core.Model;

namespace INPDS_Core.Controller
{
    public class InvoiceController : IInvoiceController
    {
        public ValidationResult RegisterInvoice(Invoice invoice)
        { var validationResult = Validate(invoice);
            if (validationResult.IsValid)
            {
                using (var context = new ReturnFreightContext())
            {
                context.Invoices.Add(invoice);
                context.SaveChanges();
            }
            }
            return validationResult;
        }

        private ValidationResult Validate(Invoice invoice)
        {
            ValidationResult result = ValidationResult.Ok();
            if (invoice.Price < 0)
            {
                result = result.JoinResults(ValidationResult.Error("Částka nebyla zadána."));
            }
            return result;
        }
    }
}
