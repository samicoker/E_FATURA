using Entities.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Response
{
    public class ReadFromArchiveResponse
    {
        public INVOICE Invoice { get; set; }
        public REQUEST_RETURN RequestReturn { get; set; }
    }
}
