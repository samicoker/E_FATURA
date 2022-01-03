using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class InvoiceRep : IInvoiceDal
    {

        #region AUTHENTICATION
        public IDataResult<RLoginRequest> GetLogin(string userName, string password)
        {
            RLoginRequest authenticationRequest = null;

            authenticationRequest = new RLoginRequest
            {
                Request_Header = new REQUEST_HEADER_AUTHENTICATION
                {
                    SESSION_ID = string.Empty,
                    CLIENT_TXN_ID = string.Empty,
                    INTL_TXN_ID = string.Empty,
                    INTL_PARENT_TXN_ID = string.Empty,
                    ACTION_DATE = string.Empty,
                    CHANGE_INFO = new CHANGE_INFO
                    {
                        CDATE = string.Empty,
                        CPOSITION_ID = string.Empty,
                        CUSER_ID = string.Empty,
                        UDATE = string.Empty,
                        UPOSITION_ID = string.Empty,
                        UUSER_ID = string.Empty
                    },
                    REASON = string.Empty,
                    APPLICATION_NAME = string.Empty,
                    HOSTNAME = string.Empty,
                    CHANNEL_NAME = string.Empty,
                    SIMULATION_FLAG = string.Empty,
                    COMPRESSED = string.Empty,
                    ATTRIBUTES = string.Empty
                },
                USER_NAME = userName, //"izibiz-test2",
                PASSWORD = password //"izi321"
            };

            return new SuccessDataResult<RLoginRequest>(authenticationRequest);
        }
        public IDataResult<RLogOutRequest> GetLogOut(string sessionId)
        {

            RLogOutRequest logOutRequest = new RLogOutRequest
            {
                Request_Header = new REQUEST_HEADER_AUTHENTICATION
                {
                    SESSION_ID = sessionId
                },
            };

            return new SuccessDataResult<RLogOutRequest>(logOutRequest);
        }
        public IDataResult<RGetGibUserListRequest> GetGibUserListRequest(string sessionId)
        {
            RGetGibUserListRequest getGibUserListRequest = new RGetGibUserListRequest
            {
                REQUEST_HEADER = new REQUEST_HEADER_AUTHENTICATION
                {
                    SESSION_ID = sessionId,
                    CLIENT_TXN_ID = string.Empty,
                    INTL_TXN_ID = string.Empty,
                    INTL_PARENT_TXN_ID = string.Empty,
                    ACTION_DATE = string.Empty,
                    CHANGE_INFO = new CHANGE_INFO
                    {
                        CDATE = string.Empty,
                        CPOSITION_ID = string.Empty,
                        CUSER_ID = string.Empty,
                        UDATE = string.Empty,
                        UPOSITION_ID = string.Empty,
                        UUSER_ID = string.Empty,
                    },
                    REASON = string.Empty,
                    APPLICATION_NAME = string.Empty,
                    HOSTNAME = string.Empty,
                    CHANNEL_NAME = string.Empty,
                    SIMULATION_FLAG = string.Empty,
                    COMPRESSED = string.Empty,
                    ATTRIBUTES = null
                },

                TYPE = "XML",
                DOCUMENT_TYPE = string.Empty,
                REGISTER_TIME_START = string.Empty,
                ALIAS_TYPE = string.Empty,
                ALIAS_MODIFY_DATE = string.Empty,
            };
            return new SuccessDataResult<RGetGibUserListRequest>(getGibUserListRequest);
        }
        #endregion

        #region E-INVOICE
        public IDataResult<RSendInvoiceRequest> SendInvoice(string sessionId, List<RINVOICE> rINVOICEs)
        {
            RSendInvoiceRequest sendInvoiceRequest = null;

            sendInvoiceRequest = new RSendInvoiceRequest
            {
                Body = new Body
                {
                    SendInvoiceRequestBody = new SendInvoiceRequestBody
                    {
                        REQUEST_HEADER = new REQUEST_HEADER_INVOICE
                        {
                            SESSION_ID = sessionId, //9fb5e098-1e3e-494b-b62e-e84a5c629c22
                            COMPRESSED = "N"
                        },
                        Invoices = rINVOICEs,                       
                    }
                }

            };
            return new SuccessDataResult<RSendInvoiceRequest>(sendInvoiceRequest);
        }
        public IDataResult<RGetInvoiceRequest> GetInvoice(string sessionId, byte limit)
        {
            RGetInvoiceRequest getInvoiceRequest = null;
            string guid = Guid.NewGuid().ToString();

            getInvoiceRequest = new RGetInvoiceRequest
            {
                Header = new HEADER(),
                Body = new BodyGet
                {
                    GetInvoiceRequest = new GetInvoiceRequestGet
                    {
                        RequestHeader = new REQUEST_HEADER_INVOICE
                        {
                            SESSION_ID = sessionId,
                            COMPRESSED = "N"
                        },
                        INVOICE_SEARCH_KEY = new INVOICE_SEARCH_KEY
                        {
                            LIMIT = limit,
                            DATE_TYPE = "CREATE",
                            START_DATE = "2021-10-01",
                            END_DATE = "2021-11-19",

                            READ_INCLUDED = "true",
                            DIRECTION = "IN"
                        },
                        HEADER_ONLY = "N"
                    }
                }
            };

            return new SuccessDataResult<RGetInvoiceRequest>(getInvoiceRequest);
        }
        public IDataResult<RMarkInvoiceRequest> GetMarkInvoice(string sessionId, List<InvoiceMark> invoices)
        {
            RMarkInvoiceRequest markInvoiceRequest = null;
            string guid = Guid.NewGuid().ToString();

            markInvoiceRequest = new RMarkInvoiceRequest
            {
                Header = new HEADER(),
                Body = new BodyMark
                {
                    MarkInvoiceRequest = new MarkInvoiceRequestMark
                    {
                        RequestHeader = new REQUEST_HEADER_INVOICE
                        {
                            SESSION_ID = sessionId,
                            COMPRESSED = "N"
                        },
                        Mark = new Mark
                        {
                            Invoices = invoices,

                        }
                    }
                }
            };

            return new SuccessDataResult<RMarkInvoiceRequest>(markInvoiceRequest);
        }
        public IDataResult<RSendInvoiceResponseWithServerSignRequest> GetSendInvoiceResponseWithServerSign(string sessionId, RINVOICE invoice, bool status)
        {
            string guid = new Guid().ToString();
            RSendInvoiceResponseWithServerSignRequest sendInvoiceResponseWithServerSignRequest = new()
            {
                Header = new HEADER
                {

                },
                REQUEST_HEADER = new REQUEST_HEADER_INVOICE
                {
                    SESSION_ID = sessionId,
                    COMPRESSED = "N"
                },
                //STATUS = "RED",
                INVOICE = new RINVOICE
                {
                    ID = invoice.ID, //"ASD2021000200592",//invoice.ID, "MES2021000000421",
                    UUID = invoice.UUID, //"227584a6-593c-4c90-94a4-9f402e7fe68c", //invoice.UUID, "0312cdb0-5c5b-41ae-913f-cb717418bcd3", 
                    HEADER = new HEADERINV
                    {
                        DIRECTION = invoice.HEADER.DIRECTION,
                    }
                },
                DESCRIPTION = "YANITIN AÇIKLAMASINI YAZDIM"
            };
            if (status == true)
            {
                sendInvoiceResponseWithServerSignRequest.STATUS = "KABUL";
            }
            else
            {
                sendInvoiceResponseWithServerSignRequest.STATUS = "RED";
            }
            return new SuccessDataResult<RSendInvoiceResponseWithServerSignRequest>(sendInvoiceResponseWithServerSignRequest);
        }
        public IDataResult<RGetInvoiceStatusRequest> GetInvoiceStatus(string sessionId, RINVOICE invoice)
        {
            RGetInvoiceStatusRequest getInvoiceStatusRequest = new RGetInvoiceStatusRequest
            {
                Header = new HEADER
                {

                },
                Body = new BodyStatus
                {
                    REQUEST_HEADER = new REQUEST_HEADER_INVOICE
                    {
                        SESSION_ID = sessionId,
                        COMPRESSED = "N"
                    },
                    INVOICE = new RINVOICE
                    {
                        HEADER = new HEADERINV
                        {
                            DIRECTION = "OUT"
                        },
                        ID = "BIL2021000000007",
                        UUID = "27ace50e-3a6c-4590-8290-7384fb784d85"
                    }
                }
            };
            return new SuccessDataResult<RGetInvoiceStatusRequest>(getInvoiceStatusRequest);
        }

        public IDataResult<RGetInvoiceStatusAllRequest> GetInvoiceStatusAllRequest(string sessionId, params string[] UUID)
        {
            RGetInvoiceStatusAllRequest rGetInvoiceStatusAllRequest = new RGetInvoiceStatusAllRequest
            {
                RequestHeader = new REQUEST_HEADER_INVOICE
                {
                    SESSION_ID = sessionId,
                    COMPRESSED = "Y"
                },
                UUID = new List<string>()
            };

            for (int i = 0; i < UUID.Length; i++)
            {
                rGetInvoiceStatusAllRequest.UUID.Add(UUID[i]);
            }

            return new SuccessDataResult<RGetInvoiceStatusAllRequest>(rGetInvoiceStatusAllRequest);
        }
        #endregion

        #region ARCHIVE
        public IDataResult<RWriteToArchieveExtendedRequest> GetWriteToArchieveExtendedRequest(string sessionId, RINVOICE rINVOICE)
        {
            string guid = Guid.NewGuid().ToString();

            RWriteToArchieveExtendedRequest rWriteToArchieveExtendedRequest = new RWriteToArchieveExtendedRequest
            {
                Body = new BodyArchive
                {
                    RequestHeader = new REQUEST_HEADER_INVOICE
                    {
                        SESSION_ID = sessionId,
                        COMPRESSED = "N"
                    },
                    ArchiveInvoiceExtendedContent = new ArchiveInvoiceExtendedContent
                    {
                        INVOICE_PROPERTIES = new INVOICE_PROPERTIES
                        {
                            EARSIV_FLAG = "Y",
                            EARSIV_PROPERTIES = new EARSIV_PROPERTIES
                            {
                                EARSIV_TYPE = "NORMAL",
                                EARSIV_EMAIL_FLAG = "N",
                                SUB_STATUS = "NEW"
                            },
                            PDF_PROPERTIES = null,
                            INVOICE_CONTENT = new INVOICE_CONTENT
                            {
                                INVOICE = rINVOICE
                            }
                        }
                    }
                }
            };

            return new SuccessDataResult<RWriteToArchieveExtendedRequest>(rWriteToArchieveExtendedRequest);
        }
        public IDataResult<RReadFromArchiveRequest> GetReadFromArchive(string sessionId, string invoiceId)
        {
            RReadFromArchiveRequest rReadFromArchiveRequest = new()
            {
                REQUEST_HEADER = new REQUEST_HEADER_INVOICE
                {
                    SESSION_ID = sessionId
                },
                INVOICEID = invoiceId,
                PORTAL_DIRECTION = "OUT",
                PROFILE = "XML"
            };

            return new SuccessDataResult<RReadFromArchiveRequest>(rReadFromArchiveRequest);
        }
        public IDataResult<RCancelEArchiveInvoiceRequest> GetCancelEArchiveInvoice(string sessionId, string uuid)
        {
            RCancelEArchiveInvoiceRequest rCancelEArchiveInvoice = new()
            {
                REQUEST_HEADER = new REQUEST_HEADER_INVOICE
                {
                    SESSION_ID = sessionId,
                },
                CancelEArsivInvoiceContent = new CancelEArsivInvoiceContent
                {
                    FATURA_UUID = uuid,
                }
            };

            return new SuccessDataResult<RCancelEArchiveInvoiceRequest>(rCancelEArchiveInvoice);
        }
        public IDataResult<RGetEArchiveInvoiceStatusRequest> GetEArchiveInvoiceStatus(string sessionId, string uuid)
        {
            RGetEArchiveInvoiceStatusRequest rGetEArchiveInvoiceStatusRequest = new()
            {
                Header = new HEADER(),
                Body = new BodyArchiveStatus
                {
                    RequestHeader = new REQUEST_HEADER_INVOICE
                    {
                        SESSION_ID = sessionId
                    },
                    UUID = uuid
                }
            };

            return new SuccessDataResult<RGetEArchiveInvoiceStatusRequest>(rGetEArchiveInvoiceStatusRequest);
        }
        public IDataResult<RGetEArchiveReportRequest> GetEArchiveReportRequest(string sessionId, string reportPeriod, string reportStatus = "Y")
        {
            RGetEArchiveReportRequest rGetEArchiveReportRequest = new RGetEArchiveReportRequest
            {
                RequestHeader = new REQUEST_HEADER_INVOICE { SESSION_ID = sessionId },
                REPORT_PERIOD = reportPeriod,
                REPORT_STATUS_FLAG = reportStatus
            };

            return new SuccessDataResult<RGetEArchiveReportRequest>(rGetEArchiveReportRequest);
        }
        public IDataResult<RReadEArchiveReportRequest> GetReadEArchiveReportRequest(string sessionId, string raporNo)
        {
            RReadEArchiveReportRequest rReadEArchiveReportRequest = new RReadEArchiveReportRequest
            {
                BodyRead = new BODYREAD
                {
                    RequestHeader = new REQUEST_HEADER_INVOICE
                    {
                        SESSION_ID = sessionId,
                    },
                    RaporNo = raporNo
                }
            };

            return new SuccessDataResult<RReadEArchiveReportRequest>(rReadEArchiveReportRequest);
        }
        public IDataResult<RGetEmailEarchiveInvoiceRequest> GetEmailEarchiveInvoiceRequest(string sessionId, string uuId, string eMail)
        {
            RGetEmailEarchiveInvoiceRequest rGetEmailEarchiveInvoiceRequest = new RGetEmailEarchiveInvoiceRequest
            {
                RequestHeader = new REQUEST_HEADER_INVOICE
                {
                    SESSION_ID = sessionId,
                },
                FATURA_UUID = uuId,
                EMAIL = eMail
            };

            return new SuccessDataResult<RGetEmailEarchiveInvoiceRequest>(rGetEmailEarchiveInvoiceRequest);
        }

        #endregion

    }
}
