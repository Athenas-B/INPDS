using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INPDS_Core.DTO;
using INPDS_Core.Model;

namespace INPDS_Core.Controller
{
    public interface IInvoiceController
    {
        ValidationResult RegisterInvoice(Invoice invoice);
    }
}
