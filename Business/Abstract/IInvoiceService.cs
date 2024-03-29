﻿using Core.Utilities.Results;
using Entities.Dtos.Request;
using Entities.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IInvoiceService
    {
        #region AUTHENTICATION
        IDataResult<LoginResponse> GetLogin(string userName, string password);
        IDataResult<LogOutResponse> GetLogOut(string sessionId);
        IDataResult<GetGibUserListResponse> GetGibUserList(string sessionId);
        #endregion

        #region INVOICE
        IDataResult<SendInvoiceResponse> SendInvoice(string sessionId, List<RINVOICE> rINVOICEs);
        IDataResult<GetInvoiceResponse> GetInvoice(string sessionId, byte limit);
        IDataResult<GetInvoiceStatusResponse> GetInvoiceStatus(string sessionId, RINVOICE invoice);
        IDataResult<MarkInvoiceResponse> MarkInvoice(string sessionId, List<InvoiceMark> invoices);
        IDataResult<SendInvoiceResponseWithServerSignResponse> SendInvoiceResponseWithServerSign(string sessionId, RINVOICE invoice, bool status);

        IDataResult<GetInvoiceStatusAllResponse> GetInvoiceStatusAll(string sessionId, params string[] UUID);
        #endregion

        #region EARCHIVE
        IDataResult<WriteToArchieveExtendedResponse> WriteToArchieveExtended(string sessionId, RINVOICE rINVOICE);
        IDataResult<ReadFromArchiveResponse> ReadFromArchive(string sessionId, string invoiceId);
        IDataResult<CancelEArchiveInvoiceResponse> CancelEArchiveInvoice(string sessionId, string uuid);
        IDataResult<GetEArchiveInvoiceStatusResponse> EArchiveInvoiceStatus(string sessionId, string uuid);
        IDataResult<GetEArchiveReportResponse> GetEArchiveReport(string sessionId, string reportPeriod, string reportStatus = "Y");
        IDataResult<ReadEArchiveReportResponse> ReadEArchiveReport(string sessionId, string raporNo);
        IDataResult<EmailEarchiveInvoiceResponse> EmailEarchiveInvoice(string sessionId, string uuId, string eMail);

        #endregion EARCHIVE
    }
}
