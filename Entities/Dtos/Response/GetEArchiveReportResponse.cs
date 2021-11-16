using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Response
{
    public class GetEArchiveReportResponse
    {
        public REPORT REPORT { get; set; }
        public REQUEST_RETURN REQUEST_RETURN { get; set; }
    }
    public class REPORT
    {
        public string REPORT_NO { get; set; }
        public string REPORT_PERIOD { get; set; }
        public string REPORT_SUB_STATUS { get; set; }
    }
}
