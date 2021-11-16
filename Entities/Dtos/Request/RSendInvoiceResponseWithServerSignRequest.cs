using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Request
{
    public class RSendInvoiceResponseWithServerSignRequest
    {
        public HEADER Header { get; set; }
        public REQUEST_HEADER_INVOICE REQUEST_HEADER { get; set; }
        public string STATUS { get; set; }
        public INVOICE INVOICE { get; set; }
        public string DESCRIPTION { get; set; }
    }
}
