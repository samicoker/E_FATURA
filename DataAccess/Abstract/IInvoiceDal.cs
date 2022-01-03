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
        #region AUTHENTICATION
        IDataResult<RLoginRequest> GetLogin(string userName, string password);
        IDataResult<RLogOutRequest> GetLogOut(string sessionId);
        IDataResult<RGetGibUserListRequest> GetGibUserListRequest(string sessionId);
        #endregion

        #region INVOICE
        IDataResult<RSendInvoiceRequest> SendInvoice(string sessionId, List<RINVOICE> rINVOICEs);
        IDataResult<RGetInvoiceRequest> GetInvoice(string sessionId, byte limit);
        IDataResult<RMarkInvoiceRequest> GetMarkInvoice(string sessionId, List<InvoiceMark> invoices);
        IDataResult<RSendInvoiceResponseWithServerSignRequest> GetSendInvoiceResponseWithServerSign(string sessionId, RINVOICE invoice, bool status);
        IDataResult<RGetInvoiceStatusRequest> GetInvoiceStatus(string sessionId, RINVOICE invoice);

        IDataResult<RGetInvoiceStatusAllRequest> GetInvoiceStatusAllRequest(string sessionId, params string[] UUID);

        #endregion

        #region ARCHIVE

        IDataResult<RWriteToArchieveExtendedRequest> GetWriteToArchieveExtendedRequest(string sessionId, RINVOICE rINVOICE);
        IDataResult<RReadFromArchiveRequest> GetReadFromArchive(string sessionId, string invoiceId);
        IDataResult<RCancelEArchiveInvoiceRequest> GetCancelEArchiveInvoice(string sessionId, string uuid);
        IDataResult<RGetEArchiveInvoiceStatusRequest> GetEArchiveInvoiceStatus(string sessionId, string uuid);
        IDataResult<RGetEArchiveReportRequest> GetEArchiveReportRequest(string sessionId, string reportPeriod, string ReportStatus = "Y");
        IDataResult<RReadEArchiveReportRequest> GetReadEArchiveReportRequest(string sessionId, string raporNo);
        IDataResult<RGetEmailEarchiveInvoiceRequest> GetEmailEarchiveInvoiceRequest(string sessionId, string uuId, string eMail);

        #endregion
    }
}
