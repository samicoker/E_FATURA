using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Request
{
    public class RGetInvoiceRequest
    {
        public HEADER Header { get; set; }
        public BodyGet Body { get; set; }
    }
    
    public class BodyGet
    {
        public GetInvoiceRequestGet GetInvoiceRequest { get; set; }
    }
    public class GetInvoiceRequestGet
    {
        public REQUEST_HEADER_INVOICE RequestHeader { get; set; }
        public INVOICE_SEARCH_KEY INVOICE_SEARCH_KEY { get; set; }
        public string HEADER_ONLY { get; set; }
    }
    
    public class INVOICE_SEARCH_KEY
    {
        public byte LIMIT { get; set; }
        public string DATE_TYPE { get; set; }
        public string START_DATE { get; set; }
        public string END_DATE { get; set; }
        public string READ_INCLUDED { get; set; }
        public string DIRECTION { get; set; }
    }
}
