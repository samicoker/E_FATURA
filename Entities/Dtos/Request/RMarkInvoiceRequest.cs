using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Request
{
    public class RMarkInvoiceRequest
    {
        public HEADER Header { get; set; }
        public BodyMark Body { get; set; }
    }
    
    public class BodyMark
    {
        public MarkInvoiceRequestMark MarkInvoiceRequest { get; set; }
    }
    public class MarkInvoiceRequestMark
    {
        public REQUEST_HEADER_INVOICE RequestHeader { get; set; }
        public Mark Mark { get; set; }
    }
    
    public class Mark
    {
        public List<InvoiceMark> Invoices { get; set; } = new List<InvoiceMark>();
    }
    public class InvoiceMark
    {
        public HeaderInvoice Header { get; set; }
        public string ID { get; set; }
        public string UUID { get; set; }
    }
    public class HeaderInvoice
    {

    }
}
