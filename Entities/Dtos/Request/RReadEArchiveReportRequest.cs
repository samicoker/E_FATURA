using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Request
{
    public class RReadEArchiveReportRequest
    {
        public HEADER Header { get; set; }
        public BODYREAD BodyRead { get; set; }
    }
    public class BODYREAD 
    {
        public REQUEST_HEADER_INVOICE RequestHeader { get; set; }
        public string RaporNo { get; set; }
    }
}
