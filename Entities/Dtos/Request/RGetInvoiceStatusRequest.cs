using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Request
{
    public class RGetInvoiceStatusRequest
    {
        public HEADER Header { get; set; }
        public BodyStatus Body { get; set; }
    }
    
    public class BodyStatus
    {
        public REQUEST_HEADER_INVOICE REQUEST_HEADER { get; set; }
        public RINVOICE INVOICE { get; set; }
    }
    
    //public class InvoiceStatus
    //{
    //    public HeaderInvoiceStatus Header { get; set; }
    //    public string ID { get; set; }
    //    public string UUID { get; set; }
    //}
    //public class HeaderInvoiceStatus
    //{
    //    public string DIRECTION { get; set; }
    //}
}
