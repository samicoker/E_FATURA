using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Response
{
    public class GetInvoiceStatusAllResponse
    {
        public List<INVOICE_STATUS> INVOICE_STATUS { get; set; }
    }
    public class INVOICE_STATUS
    {
        public STATUSHEADER HEADER { get; set; }
    }
    public class STATUSHEADER
    {
        public string STATUS { get; set; }
        public string STATUS_DESCRIPTION { get; set; }
        public string GIB_STATUS_CODE { get; set; }
        public string GIB_STATUS_DESCRIPTION { get; set; }
        public string DIRECTION { get; set; }
        public string CDATE { get; set; }
        public string ENVELOPE_IDENTIFIER { get; set; }
        public string STATUS_CODE { get; set; }
    }
}
