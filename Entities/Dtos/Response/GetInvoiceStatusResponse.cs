using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Response
{
    public class GetInvoiceStatusResponse
    {
        public string STATUS { get; set; }
        public string STATUS_DESCRIPTION { get; set; }
        public string GIB_STATUS_CODE { get; set; }
        public string GIB_STATUS_DESCRIPTION { get; set; }
        public string CDATE { get; set; }
        public string ENVELOPE_IDENTIFIER { get; set; }
        public string STATUS_CODE { get; set; }
        public string DIRECTION { get; set; }
    }
}
