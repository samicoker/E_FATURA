using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Request
{
    public class RLoginRequest
    {
        public REQUEST_HEADER_AUTHENTICATION Request_Header { get; set; }
        public string USER_NAME { get; set; }
        public string PASSWORD { get; set; }
    }
   
   
}
