using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Request
{
    public class RWriteToArchieveExtendedRequest
    {
        public HEADER Header { get; set; }
        public BodyArchive Body { get; set; }

    }
    public class BodyArchive
    {
        public REQUEST_HEADER_INVOICE RequestHeader { get; set; }
        public ArchiveInvoiceExtendedContent ArchiveInvoiceExtendedContent { get; set; }
    }
    public class ArchiveInvoiceExtendedContent
    {
        public INVOICE_PROPERTIES INVOICE_PROPERTIES { get; set; }
    }
    public class INVOICE_PROPERTIES
    {
        public string EARSIV_FLAG { get; set; }
        public EARSIV_PROPERTIES EARSIV_PROPERTIES { get; set; }
        public PDF_PROPERTIES PDF_PROPERTIES { get; set; }
        public INVOICE_CONTENT INVOICE_CONTENT { get; set; }
    }
    public class PDF_PROPERTIES
    {

    }
    public class EARSIV_PROPERTIES
    {
        public string EARSIV_TYPE { get; set; }
        public string EARSIV_EMAIL_FLAG { get; set; }
        public string SUB_STATUS { get; set; }
    }
    public class INVOICE_CONTENT
    {
        public INVOICE INVOICE { get; set; }
    }
}
