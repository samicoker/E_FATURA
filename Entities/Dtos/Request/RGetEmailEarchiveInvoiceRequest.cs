using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Request
{
    public class RGetEmailEarchiveInvoiceRequest
    {
        public REQUEST_HEADER_INVOICE RequestHeader { get; set; }
        public string FATURA_UUID { get; set; }
        public string EMAIL { get; set; }
    }
}
