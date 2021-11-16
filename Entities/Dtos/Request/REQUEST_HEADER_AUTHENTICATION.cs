using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Request
{
    public class REQUEST_HEADER_AUTHENTICATION
    {
        public string SESSION_ID { get; set; }
        public string CLIENT_TXN_ID { get; set; }
        public string INTL_TXN_ID { get; set; }
        public string INTL_PARENT_TXN_ID { get; set; }
        public string ACTION_DATE { get; set; }
        public CHANGE_INFO CHANGE_INFO { get; set; }
        public string REASON { get; set; }
        public string APPLICATION_NAME { get; set; }
        public string HOSTNAME { get; set; }
        public string CHANNEL_NAME { get; set; }
        public string SIMULATION_FLAG { get; set; }
        public string COMPRESSED { get; set; }
        public string ATTRIBUTES { get; set; }
    }
    public class CHANGE_INFO
    {
        public string CDATE { get; set; }
        public string CPOSITION_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string UDATE { get; set; }
        public string UPOSITION_ID { get; set; }
        public string UUSER_ID { get; set; }
    }
}
