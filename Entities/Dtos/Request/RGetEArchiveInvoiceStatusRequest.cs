using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Request
{
    public class RGetEArchiveInvoiceStatusRequest
    {
        public HEADER Header { get; set; }
        public BodyArchiveStatus Body { get; set; }
    }
    public class BodyArchiveStatus
    {
        public REQUEST_HEADER_INVOICE RequestHeader { get; set; }
        public string UUID { get; set; }
    }
}
