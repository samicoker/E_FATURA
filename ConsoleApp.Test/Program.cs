using Business.Abstract;
using Business.Concrete;
using Business.Extensions.Template.Xml.EFaturaXML;
using Core.Extensions.Utilities.Helpers;
using Core.Extensions.Utilities.WebAccess.SOAP;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Dtos.Request;
using Entities.Dtos.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace ConsoleApp.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            IInvoiceService example = new InvoiceManager(new InvoiceRep());

            List<InvoiceMark> invoices = new List<InvoiceMark>
            {
                new InvoiceMark()
                {
                    ID = "SDJ2021000000212",
                    UUID = "0628DABC-7A53-54D2-D2BC-A7CF8C0CE599"
                },
            };

            INVOICE invoice = new INVOICE
            {
                ID = "MES2021000000420",
                UUID = "8cd7ab00-55fa-42b0-a1bd-9f3b4c91896b", //8cd7ab00-55fa-42b0-a1bd-9f3b4c91896b
                HEADER = new HEADERINV
                {
                    DIRECTION = "IN"
                }
            };

            //string session = "8c06b950-e258-4ff5-b631-cc5aab8140d3";

            var sesres = example.GetLogin("izibiz-test2", "izi321");
            //var res = example.GetLogOut(sesres.Data.SessionId);


            //var res = example.SendInvoice(sesres.Data.SessionId);
            //var res = example.GetInvoice(sesres.Data.SessionId, 100);
            //var res = example.MarkInvoice(sesres.Data.SessionId, invoices);
            //var res = example.SendInvoiceResponseWithServerSign(sesres.Data.SessionId, invoice, false);
            //var res = example.GetInvoiceStatus(sesres.Data.SessionId, invoice);
            //var res = example.GetGibUserList(sesres.Data.SessionId);
            var res = example.GetInvoiceStatusAll(sesres.Data.SessionId, "8cd7ab00-55fa-42b0-a1bd-9f3b4c91896b");

            //var res = example.WriteToArchieveExtended(sesres.Data.SessionId);
            //var res = example.ReadFromArchive(sesres.Data.SessionId, invoice);
            //var res = example.CancelEArchiveInvoice(sesres.Data.SessionId, invoice.UUID);
            //var res = example.EArchiveInvoiceStatus(sesres.Data.SessionId, invoice.UUID);
            //var res = example.GetEArchiveReport(sesres.Data.SessionId, "202110"); // 202110
            //var res = example.ReadEArchiveReport(sesres.Data.SessionId, "b251241e-3439-4c54-a6dd-ba52a690f8d3");
            //var res = example.EmailEarchiveInvoice(sesres.Data.SessionId, "93C30A9C-12C4-4C75-B97C-51013D2FA45F", "a@a.com.tr");

            var sonuc = res.Data;
            //var sessionId = sonuc.SessionId;
        }

    }
}
