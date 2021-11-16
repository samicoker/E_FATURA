using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Request
{
    public class RCancelEArchiveInvoiceRequest
    {
        public REQUEST_HEADER_INVOICE REQUEST_HEADER { get; set; }
        public CancelEArsivInvoiceContent CancelEArsivInvoiceContent { get; set; }
    }
    public class CancelEArsivInvoiceContent
    {
        public string FATURA_UUID { get; set; }
    }
}
