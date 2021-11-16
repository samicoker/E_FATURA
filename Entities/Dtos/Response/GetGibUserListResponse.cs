using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Response
{
    public class GetGibUserListResponse
    {
        public string CONTENT { get; set; }
        public ERROR_TYPE ErrorType { get; set; }
    }
    public class ERROR_TYPE
    {
        public string INTL_TXN_ID { get; set; }
        public string ERROR_CODE { get; set; }
        public string ERROR_SHORT_DES { get; set; }
    }
}
