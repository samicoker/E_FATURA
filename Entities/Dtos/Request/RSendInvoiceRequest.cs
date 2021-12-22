using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Request
{
    public class RSendInvoiceRequest
    {
        public HEADER Header { get; set; }
        public Body Body { get; set; }

    }
    public class Body
    {
        public SendInvoiceRequestBody SendInvoiceRequestBody { get; set; }
    }
    public class SendInvoiceRequestBody
    {
        public REQUEST_HEADER_INVOICE REQUEST_HEADER { get; set; }
        public RINVOICE INVOICE { get; set; }
        public List<RINVOICE> Invoices { get; set; } 
    }
    
   
    
    //public class Header
    //{

    //}
    
}
