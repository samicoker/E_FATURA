using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Response
{
    public class WriteToArchieveExtendedResponse
    {
        public REQUEST_RETURN REQUEST_RETURN { get; set; }
        public string INVOICE_ID { get; set; }
        public ERROR_TYPE ErrorType { get; set; }
    }
    
}
