using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Request
{
    public class RGetInvoiceStatusAllRequest
    {
        public REQUEST_HEADER_INVOICE RequestHeader { get; set; }
        public List<string> UUID { get; set; }
    }
}
