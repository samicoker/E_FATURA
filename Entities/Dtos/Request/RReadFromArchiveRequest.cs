using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Request
{
    public class RReadFromArchiveRequest
    {
        public HEADER Header { get; set; }
        public REQUEST_HEADER_INVOICE REQUEST_HEADER { get; set; }
        public string INVOICEID { get; set; }
        public string PORTAL_DIRECTION { get; set; }
        public string PROFILE { get; set; }
    }
}
