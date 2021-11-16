using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Response
{
    public class MarkInvoiceResponse
    {
        public REQUEST_RETURN REQUEST_RETURN { get; set; }
    }
    public class REQUEST_RETURN
    {
        public string INTL_TXN_ID { get; set; }
        public string RETURN_CODE { get; set; }
        public string CLIENT_TXN_ID { get; set; }
    }
}
