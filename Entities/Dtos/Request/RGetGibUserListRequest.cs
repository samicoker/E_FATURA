using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Request
{
    public class RGetGibUserListRequest
    {
        public REQUEST_HEADER_AUTHENTICATION REQUEST_HEADER { get; set; }
        public string TYPE { get; set; }
        public string DOCUMENT_TYPE { get; set; }
        public string REGISTER_TIME_START { get; set; }
        public string ALIAS_TYPE { get; set; }
        public string ALIAS_MODIFY_DATE { get; set; }
    }
   
    public class ATTRIBUTE
    {

    }
    
}
