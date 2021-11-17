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
        IDataResult<LoginResponse> GetLogin(string userName, string password);
        IDataResult<LogOutResponse> GetLogOut(string sessionId);
        IDataResult<SendInvoiceResponse> SendInvoice(string sessionId);
        IDataResult<GetInvoiceResponse> GetInvoice(string sessionId, byte limit);
        IDataResult<GetInvoiceStatusResponse> GetInvoiceStatus(string sessionId, INVOICE invoice);
        IDataResult<MarkInvoiceResponse> MarkInvoice(string sessionId, List<InvoiceMark> invoices);
        IDataResult<SendInvoiceResponseWithServerSignResponse> SendInvoiceResponseWithServerSign(string sessionId, INVOICE invoice, bool status);
        IDataResult<GetGibUserListResponse> GetGibUserList(string sessionId);

        //EARCHIVE
        IDataResult<WriteToArchieveExtendedResponse> WriteToArchieveExtended(string sessionId);
        IDataResult<ReadFromArchiveResponse> ReadFromArchive(string sessionId, INVOICE Invoice);
        IDataResult<CancelEArchiveInvoiceResponse> CancelEArchiveInvoice(string sessionId, string uuid);
        IDataResult<GetEArchiveInvoiceStatusResponse> EArchiveInvoiceStatus(string sessionId, string uuid);
        IDataResult<GetEArchiveReportResponse> EArchiveReport(string sessionId, string reportPeriod, string reportStatus = "Y");
        IDataResult<ReadEArchiveReportResponse> ReadEArchiveReport(string sessionId, string raporNo);
        IDataResult<EmailEarchiveInvoiceResponse> EmailEarchiveInvoice(string sessionId, string uuId, string eMail);

        //EARCHIVE
    }
}
