using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Response
{
    public class GetEArchiveInvoiceStatusResponse
    {
        public string INVOICE_ID { get; set; }
        public string PROFILE { get; set; }
        public string UUID { get; set; }
        public string INVOICE_DATE { get; set; }
        public string STATUS { get; set; }
        public string STATUS_DESC { get; set; }
        public string EMAIL_STATUS { get; set; }
        public string EMAIL_STATUS_DESC { get; set; }
        public string REPORT_ID { get; set; }
        public string WEB_KEY { get; set; }
    }
}
