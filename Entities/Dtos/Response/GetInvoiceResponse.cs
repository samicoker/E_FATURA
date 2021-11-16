using Entities.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Response
{
    public class GetInvoiceResponse
    {
        //public GetInvoiceResponseTag GetInvoiceResponseTag { get; set; }
        public List<Invoice> Invoices { get; set; }
    }
    public class GetInvoiceResponseTag
    {
        public List<Invoice> Invoices { get; set; }
    }
    public class Invoice
    {
        public Header Header { get; set; }
        public CONTENT CONTENT { get; set; }
    }
    
    public class Header
    {
        public string SENDER { get; set; }
        public string RECEIVER { get; set; }
        public string SUPPLIER { get; set; }
        public string CUSTOMER { get; set; }
        public string ISSUE_DATE { get; set; }
        public string PAYABLE_AMOUNT { get; set; }
        public string FROM { get; set; }
        public string TO { get; set; }
        public string PROFILEID { get; set; }
        public string INVOICE_TYPE_CODE { get; set; }
        public string STATUS { get; set; }
        public string STATUS_DESCRIPTION { get; set; }
        public string GIB_STATUS_CODE { get; set; }
        public string GIB_STATUS_DESCRIPTION { get; set; }
        public string CDATE { get; set; }
        public string ENVELOPE_IDENTIFIER { get; set; }
        public string STATUS_CODE { get; set; }
    }
   
}
