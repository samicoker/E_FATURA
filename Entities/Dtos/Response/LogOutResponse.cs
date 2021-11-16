using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Response
{
    public class LogOutResponse
    {
        public REQUEST_RETURN RequestReturn { get; set; }
        public ERROR_TYPE ErrorType { get; set; }
    }
}
