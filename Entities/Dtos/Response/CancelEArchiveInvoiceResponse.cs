using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Response
{
    public class CancelEArchiveInvoiceResponse
    {
        public REQUEST_RETURN Request_Return { get; set; }
        public ERROR_TYPE ERROR_TYPE { get; set; }
    }
}
