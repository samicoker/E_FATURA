using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Request
{
    public class RGetEArchiveReportRequest
    {
        public REQUEST_HEADER_INVOICE RequestHeader { get; set; }
        public string REPORT_PERIOD { get; set; }
        public string REPORT_STATUS_FLAG { get; set; }
    }
}
