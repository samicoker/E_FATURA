using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Response
{
    public class SendInvoiceResponse
    {
        public string INTL_TXN_ID { get; set; }
        public string RETURN_CODE { get; set; }
        public string ERROR_CODE { get; set; }
        public string ERROR_SHORT_DES { get; set; }
        public string ERROR_LONG_DES { get; set; }
    }
}
