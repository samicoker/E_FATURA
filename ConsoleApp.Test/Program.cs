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
                UUID = "8cd7ab00-55fa-42b0-a1bd-9f3b4c91896b",
                HEADER = new HEADERINV
                {
                    DIRECTION = "IN"
                }
            };

            string session = "4c012732-a3fc-4b90-9006-131c4315ed5d";

            //var res = example.GetLogin("izibiz-test2", "izi321");
            //var res = example.GetLogOut(session);
            //var res = example.SendInvoice(session);
            //var res = example.GetInvoice(session, 1);
            //var res = example.MarkInvoice(session, invoices);
            //var res = example.GetGibUserList(session);
            //var res = example.GetInvoiceStatus(session, invoice);
            //var res = example.SendInvoiceResponseWithServerSign(session, invoice, false);

            // E-Archive

            //var res = example.WriteToArchieveExtended(session);
            //var res = example.ReadFromArchive(session,invoice);
            //var res = example.CancelEArchiveInvoice(session, invoice.UUID);
            //var res = example.EArchiveInvoiceStatus(session, invoice.UUID);
            var res = example.EArchiveReport(session, "202110");

            var sonuc = res.Data;
            //var sessionId = sonuc.SessionId;

            // E-Archive
        }

    }
}
