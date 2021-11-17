using Core.Utilities.Results;
using Entities.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IInvoiceDal
    {
        // AUTHENTICATION
        IDataResult<RLoginRequest> GetLogin(string userName, string password);
        IDataResult<RLogOutRequest> GetLogOut(string sessionId);
        // AUTHENTICATION

        // INVOICE
        IDataResult<RSendInvoiceRequest> SendInvoice(string sessionId);
        IDataResult<RGetInvoiceRequest> GetInvoice(string sessionId, byte limit);
        IDataResult<RMarkInvoiceRequest> GetMarkInvoice(string sessionId, List<InvoiceMark> invoices);
        IDataResult<RSendInvoiceResponseWithServerSignRequest> GetSendInvoiceResponseWithServerSign(string sessionId, INVOICE invoice, bool status);
        IDataResult<RGetInvoiceStatusRequest> GetInvoiceStatus(string sessionId, INVOICE invoice);
        IDataResult<RGetGibUserListRequest> GetGibUserListRequest(string sessionId);

        // INVOICE

        // ARCHIVE

        IDataResult<RWriteToArchieveExtendedRequest> GetWriteToArchieveExtendedRequest(string sessionId);
        IDataResult<RReadFromArchiveRequest> GetReadFromArchive(string sessionId, string invoiceId);
        IDataResult<RCancelEArchiveInvoiceRequest> GetCancelEArchiveInvoice(string sessionId, string uuid);
        IDataResult<RGetEArchiveInvoiceStatusRequest> GetEArchiveInvoiceStatus(string sessionId, string uuid);
        IDataResult<RGetEArchiveReportRequest> GetEArchiveReportRequest(string sessionId, string reportPeriod, string ReportStatus= "Y");
        IDataResult<RReadEArchiveReportRequest> GetReadEArchiveReportRequest(string sessionId, string raporNo);
        IDataResult<RGetEmailEarchiveInvoiceRequest> GetEmailEarchiveInvoiceRequest(string sessionId, string uuId, string eMail);

        // ARCHIVE
    }
}
